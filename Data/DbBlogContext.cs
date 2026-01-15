using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Blog.Data
    {
    public class DbBlogContext : DbContext
        {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public static string ConnectionString = "Server=develope.database.windows.net;Database=develope;User ID=dev;Password=Silvan@12345";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            optionsBuilder.UseSqlServer(ConnectionString);
            }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
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
