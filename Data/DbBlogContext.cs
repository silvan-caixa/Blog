using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data
    {
    public class DbBlogContext : DbContext
        {
        public static string ConnectionString = "Server=develope.database.windows.net;Database=develope;User ID=dev;Password=Silvan@12345";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            optionsBuilder.UseSqlServer(ConnectionString);
            }

        // Test conexao
        public static void TextConexao()
            {
            using var conexao = new SqlConnection(ConnectionString);
            try
                {
                conexao.Open();
                Console.WriteLine("Conexão aberta com sucesso.");
                }
            catch (Exception ex)
                {
                Console.WriteLine($"Erro ao abrir a conexão: {ex.Message}");
                }
            finally
                {
                conexao.Close();
                Console.WriteLine("Conexão fechada.");
                }

            }
        }
    }
