var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Testa conexao com o banco de dados
//DbBlogContext.TextConexao();

app.MapGet("/", () => "Hello World!");

app.Run();
