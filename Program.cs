using Blog.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DbBlogContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

//Testa conexao com o banco de dados
//DbBlogContext.TextConexao();

//app.MapGet("/", () => "Hello World!");

app.Run();
