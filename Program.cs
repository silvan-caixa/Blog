using Blog.Data;
using Blog.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(
         options =>
         {
             options.SuppressModelStateInvalidFilter = true;
         })
    ;
builder.Services.AddDbContext<DbBlogContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<TokenService>();

var app = builder.Build();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

//Testa conexao com o banco de dados
//DbBlogContext.TextConexao();

//app.MapGet("/", () => "Hello World!");

app.Run();
