using CadastroCliente.Pages.clientes;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CadastroCliente.Pages.Clientes
{
    public class CreateModel : PageModel
    {
        public ClienteInfo clienteInfo = new ClienteInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            clienteInfo.name = Request.Form["name"];
            clienteInfo.email = Request.Form["email"];
            clienteInfo.phone = Request.Form["phone"];
            clienteInfo.adress = Request.Form["adress"];

            if (clienteInfo.name.Length == 0 || clienteInfo.email.Length == 0 ||
                clienteInfo.phone.Length == 0 || clienteInfo.adress.Length == 0)
            {
                errorMessage = "Todos os campos são necessários preencher.";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CadastroCliente;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clientes " +
                        "(name, email, phone, adress) VALUES " +
                        "(@name, @email, @phone, @adress)";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clienteInfo.name);
                        command.Parameters.AddWithValue("@email", clienteInfo.email);
                        command.Parameters.AddWithValue("@phone", clienteInfo.phone);
                        command.Parameters.AddWithValue("@adress", clienteInfo.adress);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            clienteInfo.name = "";
            clienteInfo.email = "";
            clienteInfo.phone = "";
            clienteInfo.adress = "";

            successMessage = " Novo cliente adicionado com sucesso.";

            Response.Redirect("/Clientes/Index");
        }
    }
}
