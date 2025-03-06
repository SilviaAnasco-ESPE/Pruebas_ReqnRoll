using System.Data.SqlClient;
using TDDTestingMVC2.Data;

namespace TDDTestingMVC2.Models
{
    public class PedidosDataAccessLayer
    {
        private readonly string connectionString = "Data Source=DESKTOPSILV;Initial Catalog=Producto;User ID=sa; Password=sqlserver";

        public List<Pedidos> GetAllPedidos()
        {
            List<Pedidos> listaPedidos = new List<Pedidos>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("pedido_SelectAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Pedidos pedido = new Pedidos
                    {
                        PedidoID = Convert.ToInt32(rdr["PedidoID"]),
                        ClienteID = Convert.ToInt32(rdr["ClienteID"]),
                        FechaPedido = Convert.ToDateTime(rdr["FechaPedido"]),
                        Monto = Convert.ToDecimal(rdr["Monto"]),
                        Estado = rdr["Estado"].ToString()
                    };
                    listaPedidos.Add(pedido);
                }
                con.Close();
            }
            return listaPedidos;
        }

        public void AddPedido(Pedidos pedido)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Verificar si el ClienteID existe
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Cliente WHERE Codigo = @ClienteID", con);
                checkCmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);

                int exists = (int)checkCmd.ExecuteScalar();

                if (exists == 0)
                {
                    throw new Exception("El ClienteID no existe.");
                }

                // Insertar el pedido si el ClienteID es válido
                SqlCommand cmd = new SqlCommand("pedido_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado);

                cmd.ExecuteNonQuery();
            }
        }


        public Pedidos getPedidoById(int codigo)
        {
            Pedidos pedido = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("pedido_SelectById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PedidoID", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    pedido = new Pedidos
                    {
                        PedidoID = Convert.ToInt32(rdr["PedidoID"]),
                        ClienteID = Convert.ToInt32(rdr["ClienteID"]),
                        FechaPedido = Convert.ToDateTime(rdr["FechaPedido"]),
                        Monto = Convert.ToDecimal(rdr["Monto"]),
                        Estado = rdr["Estado"].ToString()
                    };
                }
                con.Close();
            }
            return pedido;
        }

        public void UpdatePedido(Pedidos pedido)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open(); 

                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Cliente WHERE Codigo = @ClienteID", con);
                checkCmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);

                int exists = (int)(checkCmd.ExecuteScalar() ?? 0); 

                if (exists == 0)
                {
                    throw new Exception($"El ClienteID {pedido.ClienteID} no existe.");
                }

                
                SqlCommand cmd = new SqlCommand("pedido_Update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PedidoID", pedido.PedidoID);
                cmd.Parameters.AddWithValue("@ClienteID", pedido.ClienteID);
                cmd.Parameters.AddWithValue("@Monto", pedido.Monto);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado);

                cmd.ExecuteNonQuery();
            } 
        }


        public void DeletePedido(int pedidoID)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("pedido_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PedidoID", pedidoID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
