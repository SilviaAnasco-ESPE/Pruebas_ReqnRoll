using Microsoft.AspNetCore.Mvc;
using TDDTestingMVC2.Data;
using TDDTestingMVC2.Models;

namespace TDDTestingMVC2.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDataAccessLayer objClienteDAL = new ClienteDataAccessLayer();


        public IActionResult Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes = objClienteDAL.getAllCliente().ToList();
            return View(clientes);
        }

        public IActionResult Create() { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Cliente objCliente) {
            if (ModelState.IsValid) { 
                objClienteDAL.AddCliente(objCliente);
                return RedirectToAction("Index");
            }
            return View(objCliente);
        }

        public IActionResult Edit(int? codigo)
        {
            if (codigo == null)
            {
                return NotFound();
            }

            Cliente cliente = objClienteDAL.getClienteById(codigo.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Cliente objCliente)
        {
            if (ModelState.IsValid)
            {
                objClienteDAL.updateCliente(objCliente);
                return RedirectToAction("Index");
            }
            return View(objCliente);
        }


        //Eliminar
        public IActionResult Delete(int? codigo)
        {
            if (codigo == null)
            {
                return NotFound();
            }

            Cliente cliente = objClienteDAL.getClienteById(codigo.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int codigo)
        {
            objClienteDAL.deleteCliente(codigo);
            return RedirectToAction("Index");
        }

    }
}
