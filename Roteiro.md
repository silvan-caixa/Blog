# Roteiro do projeto
## 1º ETAPA
### Crud e Entity Framework
### Crinado projeto
	- dotnet new web -o Blog
### Adiocionando Suporte ao Entity Framework
	- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	- dotnet add package Microsoft.EntityFrameworkCore.Tools
	- dotnet add package Microsoft.EntityFrameworkCore.Design
### Criando banco de dados 
	- Cria pasta Data
	- Cria classe BlogContext.cs
	- Testando a conexao com o banco de dados

## 2º ETAPA
### Criar pastas
	- Models
		- Categoria.cs
			- Id, Nome, Slug, DataCriacao
	- Controllers
		- HomeController.cs
		- CategoriasController.cs
	- Data
		- BlogContext.cs
		- Mapping
			- CategoriaMap.cs
	- Config
		- AppSettings.cs
	- Migrations
		- dotnet ef migrations add InitialCreate
		- dotnet ef database update

## 3º ETAPA
### Revisar as aulas do modulo CRUD e Entity Framework
	- Cria projeto web
	- Aula 3 pegando o projeto antigo
	- Inclui no program:	
		- builder.Services.AddControllers(); => Adiciona os edpoints dos controllers
		- builde.Services.AddDbContext<DbBlogContext>; => Adiciona o contexto do banco de dados
		- app.MapControllers(); => Mapeia os endpoints dos controllers
	- Cria pasta Controllers
		- Cria HomeController.cs
			- herança : ControllerBase
			- [ApiController]
			- [Route("")]
			- [HttpGet("")]
			- public IActionResult Get() => Ok("API Funcionando");
		- Cria CategoriasController.cs
			- herança : ControllerBase
			- [ApiController] => fica sobre a classe
			- [Route("")]
			- [HttpGet("categories")]
			- public IActionResult Get([FromServices] DbBlogContext context) 
				{	
					var categories = context.Categories.ToList();
					return Ok(categories); 
				};
			- Versionamento de rota
				- [Route("v1")]
				- [HttpGet("categories")]
				
			- Async e Await
				- public async Task<IActionResult> GetAsync([FromServices] DbBlogContext context) 
				{	
					var categories = await context.Categories.ToListAsync();
					return Ok(categories); 
				};
			- Crud Completo
				- GetAsync
				- GetByIdAsync([FromRoute] int id, [FromServices] DbBlogContext context)
					- [HttpGet("categories/{id:int}")] => fica sobre a funcao
					- var categoria = wait context.Categories.FirstOrDefaultAsync(x => x.Id == id);
					- return Ok(categoria)
					


				- PostAsync
					- [HttpPost("categories")]
					- public async Task<IActionResult> PostAsync([FromBody] Categoria model, [FromServices] DbBlogContext context)
					- await context.Categories.Add(model);
					- await context.SaveChangesAsync();
					- return Created($"v1/categories/{model.Id}", model);

				- PutAsync
					- [HttpPut("categories/{id:int}")]
					- public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Categoria model, [FromServices] DbBlogContext context)
					- var categoria = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
					- if(categoria == null)
						- return NotFound();
					- categoria.Nome = model.Nome;
					- categoria.Slug = model.Slug;
					- context.Categories.Update(categoria);
					- await context.SaveChangesAsync();
					- return Ok(categoria);

				- DeleteAsync
					- [HttpDelete("categories/{id:int}")]
					- public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] DbBlogContext context)
					- var categoria = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
					- if(categoria == null)
						- return NotFound();
					- context.Categories.Remove(categoria);
					- await context.SaveChangesAsync();
					- return Ok(categoria);
					
				
### Instalar Swagger
	- dotnet add package Swashbuckle.AspNetCore
	- 
	- Program.cs
		- builder.Services.AddEndpointsApiExplorer();
		- builder.Services.AddSwaggerGen();
		- app.UseSwagger();
		- app.UseSwaggerUI();


### Configurando a string de conexão
	- No arquivo appsettings.json, adiciona a string de conexão
### Iniciando os Controllers
	- Program.cs
		- Adiciona o endpoint padrão

## 4º ETAPA
### Tratando Erros
	- try{} catch{}
	- Incluir código de error
	- Test API

## 5º ETAPA
### Revisar aula a partir de VIEWMODELS
### View Model
	- Cria pasta ViewModels
	- Cria a classe CreateCategoriaViewModel.cs
		- prop Nome e prop slug
	- Configura no CategoriaController o metodo post com os parametro para receber ViewModels
### Editor ViewModel
	- Validacao
		- [Required(ErroMessage = "Campo Obrigatório")]
		- [StringLength(40, MinimumLength = 3, ErrorMessage = "Campo minimo 3 maximo 40 caracter")]

## 6º ETAPA
	- Patronizando erros
	
		
	- Desabilita no program.cs o ModelState, precisa explicitar esse código nos metodos do controller:
		
		- no Program:
			- AddControllers()
				.ConfigureApiBehaviorOptions(options => {
					options.SuppressModelStateInvalidFilter = true;
				})

	- Cria no ViewModels a class ResultViewModel.cs
		- Implementa metoda data e errors
		- Cria classe ResultViewModel<T> no ViewModels com 4 construtores
		- ResultViewModel(T data, List<string>errors){Data = data, Errors = errors}
		- ResultViewModel(T data){Data = data}
		- ResultViewModel(List<string>errors){Errors = errors}
		- ResultViewModel(string error){Errors = Add(error)}
		- prop T Data
		- prop List<string> Errors
	- Metodo no Post
		- if(!ModelState.Isvalid) return BadRequest();
		- if(!ModelState.Isvalid) return BadRequest(ModelState.Values);
	- Cria pasta Extensions - utilizar no post
		- Cria classe ModelStateExtensions.cs
			- public static List<string> GetErrors(this ModelStateDictionary modelState)
				- var errors = new List<string>();
				- foreach(var state in modelState.Values)
					- foreach(var error in state.Errors)
						- errors.Add(error.ErrorMessage);
				- return errors;
	- Finalizado parcialmente 6º ETAPA - pedente Post(BadRequest) Put e Delete
