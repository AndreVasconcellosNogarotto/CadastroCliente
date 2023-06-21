using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CadastroCliente.Pages.clientes
{  

    public class IndexModel : PageModel
    {
        public List<ClienteInfo> listClientes = new List<ClienteInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=CadastroCliente;Integrated Security=True";

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM clientes";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClienteInfo clienteInfo = new ClienteInfo();
                                clienteInfo.id = "" + reader.GetInt32(0);
                                clienteInfo.name = reader.GetString(1);
                                clienteInfo.email = reader.GetString(2);
                                clienteInfo.phone = reader.GetString(3);
                                clienteInfo.adress = reader.GetString(4);
                                clienteInfo.created_at = reader.GetDateTime(5).ToString();

                                listClientes.Add(clienteInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro:" + ex.ToString());
            }
        }
    }

    public class ClienteInfo
    {
        public String id;
        public String name;
        public String email;
        public String phone;
        public String adress;
        public String created_at;

    }
}
