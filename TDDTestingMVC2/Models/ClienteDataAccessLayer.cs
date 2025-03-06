using Microsoft.AspNetCore.CookiePolicy;
using System.Data.SqlClient;
using TDDTestingMVC2.Data;

namespace TDDTestingMVC2.Models
{
    public class ClienteDataAccessLayer
    {
        string connectionString = "Data Source=DESKTOPSILV;Initial Catalog=Producto;User ID=sa; Password=sqlserver";

        public List<Cliente> getAllCliente()
        {
            List<Cliente> listaCliente = new List<Cliente>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectAll", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Cliente cliente = new Cliente();

                    cliente.Codigo = Convert.ToInt32(rdr["Codigo"]);
                    cliente.Cedula = rdr["Cedula"].ToString();
                    cliente.Apellidos = rdr["Apellidos"].ToString();
                    cliente.Nombres = rdr["Nombres"].ToString();
                    cliente.FechaNacimiento = Convert.ToDateTime(rdr["FechaNacimiento"]);
                    cliente.Mail = rdr["Mail"].ToString();
                    cliente.Telefono = rdr["Telefono"].ToString();
                    cliente.Direccion = rdr["Direccion"].ToString();
                    cliente.Estado = Convert.ToBoolean(rdr["Estado"]);

                    listaCliente.Add(cliente);
                }

                con.Close();
            }

            return listaCliente;
        }

        public void AddCliente(Cliente cliente) {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public Cliente getClienteById(int codigo)
        {
            Cliente cliente = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_SelectById", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cliente = new Cliente
                    {
                        Codigo = Convert.ToInt32(rdr["Codigo"]),
                        Cedula = rdr["Cedula"].ToString(),
                        Apellidos = rdr["Apellidos"].ToString(),
                        Nombres = rdr["Nombres"].ToString(),
                        FechaNacimiento = Convert.ToDateTime(rdr["FechaNacimiento"]),
                        Mail = rdr["Mail"].ToString(),
                        Telefono = rdr["Telefono"].ToString(),
                        Direccion = rdr["Direccion"].ToString(),
                        Estado = Convert.ToBoolean(rdr["Estado"])
                    };
                }
                con.Close();
            }
            return cliente;
        }

        //Actualizar cliente
        public void updateCliente(Cliente cliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", cliente.Codigo);
                cmd.Parameters.AddWithValue("@Cedula", cliente.Cedula);
                cmd.Parameters.AddWithValue("@Apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@Nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@FechaNacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Mail", cliente.Mail);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Direccion", cliente.Direccion);
                cmd.Parameters.AddWithValue("@Estado", cliente.Estado);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Eliminar cliente
        public void deleteCliente(int? codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("cliente_Delete", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
