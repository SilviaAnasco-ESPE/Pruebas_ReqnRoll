using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TDDTestingMVC2.Data;
using TDDTestingMVC2.Models;

namespace TDDTestingMVC2.Controllers
{
    public class PedidosController : Controller
    {
        PedidosDataAccessLayer objPedidosDAL = new PedidosDataAccessLayer();

        // GET: PedidosController
        public ActionResult Index()
        {
            List<Pedidos> pedidos = new List<Pedidos>();
            pedidos = objPedidosDAL.GetAllPedidos().ToList();
            return View(pedidos);
        }

        // GET: PedidosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PedidosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PedidosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] Pedidos objPedidos)
        {

            if (!ModelState.IsValid)
            {
                return View(objPedidos);
            }

            try
            {
                objPedidosDAL.AddPedido(objPedidos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ClienteID", ex.Message); // Agrega el mensaje de error al campo ClienteID
                return View(objPedidos);
            }
        }

        // GET: PedidosController/Edit/5
        public ActionResult Edit(int? codigo)
        {
            if (codigo == null)
            {
                return NotFound();
            }

            Pedidos pedido = objPedidosDAL.getPedidoById(codigo.Value);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: PedidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pedidos objPedido)
        {
            if (!ModelState.IsValid)
            {
                return View(objPedido);
            }

            try
            {
                objPedidosDAL.UpdatePedido(objPedido);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ClienteID", ex.Message); // Agrega el mensaje de error al campo ClienteID
                return View(objPedido);
            }

            
        }

        // GET: PedidosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PedidosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
