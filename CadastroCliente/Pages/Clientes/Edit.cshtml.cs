using CadastroCliente.Pages.clientes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CadastroCliente.Pages.Clientes
{
    public class EditModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CadastroCliente;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    String sql = "SELECT * FROM clientes WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.name = reader.GetString(1);
                                clienteInfo.email = reader.GetString(2);
                                clienteInfo.phone = reader.GetString(3);
                                clienteInfo.adress = reader.GetString(4);                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }

        }

        public void OnPost()
        {
            clienteInfo.id = Request.Form["id"];
            clienteInfo.name = Request.Form["name"];
            clienteInfo.email = Request.Form["email"];
            clienteInfo.phone = Request.Form["phone"];
            clienteInfo.adress = Request.Form["adress"];

            if (clienteInfo.id.Length == 0 || clienteInfo.name.Length == 0 || clienteInfo.email.Length == 0 ||
               clienteInfo.phone.Length == 0 || clienteInfo.adress.Length == 0)
            {
                errorMessage = "Todos os campos são necessários preencher.";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CadastroCliente;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE clientes " +
                        "SET name=@name, email=@email, phone=@phone, adress=@adress " +
                        "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clienteInfo.name);
                        command.Parameters.AddWithValue("@email", clienteInfo.email);
                        command.Parameters.AddWithValue("@phone", clienteInfo.phone);
                        command.Parameters.AddWithValue("@adress", clienteInfo.adress);
                        command.Parameters.AddWithValue("@id", clienteInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Clientes/Index");

        }
    }
}
