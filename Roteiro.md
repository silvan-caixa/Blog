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
# PASTAS IMPLEMENTADA NO PROJETO ATÉ AQUI
	- DATA
		- CONTROLLERS
		- MAPPING
		- ARQUIVO
			- DbBlogContext.cs
	- MODELS
		- Arquivos
			-	Categoria.cs
	- CONTROLLERS
		- Arquivos
			- HomeController.cs
			- CategoriasController.cs
	- VIEWMODELS
		- Arquivos
			- CreateCategoriaViewModel.cs
			- ResultViewModel.cs
			
	- EXTENSIONS
		- Arquivos
			- ModelStateExtensions.cs
	- MIGRATIONS
		- Arquivos
			- InitialCreate.cs
			- BlogContextModelSnapshot.cs

## 7º ETAPA
### Revisar aula a partir de AUTENTICAÇÃO E AUTORIZAÇÃO
	- Utiliza IDE Rider
	- Cria banco (Category, User, Role)
		- Models
			- Category => Name, Slug, CreatedAt
			- User => Id, Name, Email, PasswordHash, Slug, Bio, Image, CreatedAt
			- Tag => Id, Name, Slug, CreatedAt
			- Role => Id, Name, Slug, CreatedAt
			- Post => Id, Title, Summary, Body, Slug, CreateAt, LastUpdateAt, CategoryId, AuthorId
			- PostTag => PostId, TagId
		- Mappings
			- CategoryMap
			- UserMap
			- TagMap
			- RoleMap
			- PostMap
			- PostTagMap
	- Aula TOKEN e JWT
		- Cria na raiz Configuration.cs
			- class static
			- prop string JwtKey = "esta é a chave secreta do meu token"
			- 

### Criar pasta Services
	- Instala pacote
		- dotnet add package Microsoft.AspNetCore.Authentication
		- dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
	- Cria class TokenService.cs
		- Metodo public string GenerateToken(User user)
			cria estancia:
				- var tokenHandler = new JwtSecurityTokenHandler();
				// Convert para array de bytes
				- var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
				// Esse item contém todo configuração do token
				- var TokenDescriptor = new SecurityTokenDescriptor
				{
				Expires = DateTime.UtcNow.AddHours(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorthms.HmacSha256Signature)	
				};
				var token = tokenHandler.CreateToken(tokenDescriptor);
				return tokenHandler.WriteToken(token);
### Implementar o conceito de injeção de independência
		- Program.cs
			//- builder.Services.AddScoped<TokenService>(); // cria uma instancia por requisicao
			- builder.Services.AddTransient<TokenService>() // cria uma nova instancia
			//- builder.Services.AddSingleton() // cria uma unica instancia para toda aplicação

### Controller
			- cria a class AccountController.cs : ControllerBase
				- prop private readonly TokenService _tokenService;
				- construtor public AccountController(TokenService tokenService)
					- _tokenService = tokenService;
				- [ApiController] => Fica sobre a class
				- [RouterPost("v1/login")] => Fica sobre o metodo
				- metodo public IActionResult Login([FromServices] TokenService tokenService)					
					//- var tokenService = new TokenService();
					- var token = _tokenService.GenerateToken(new User());
					- return Ok(token)

### Finalizado fase de configuração do Token tentar implementar

### Proxima aula
### Inspecionando o TOKEN
	- Testa no Postman
	- site jwt.io // inspeciona o token

### No TokenService.cs
    - No tokenDescriptor
        - inclui 
                - Subject = new ClaimsIdentity(new Claim[])
        {
        new Claim("ClaimTypes.Name","silvasilva");
        new Claim("ClaimTypes.Role","admin");
        new Claim ("Fruta", "banana")
       }
### Libera autorização e autenticação da aplicação é no Program.cs 
    - var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
    - builder.Services.AddAuthenticate(x=>{ 
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
    x.DefaultChallengeScheme = JwtBearerDefault.AuthenticationSchema;
    }).AddJwtBearer(x=>{
    x.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    }
    });
    - app.UseAuthentication();
    - app.UseAuthorization();

### Encerrando essa etapa. Parei na aula Configurando autenticação e autorização
    - feito somente os apontamento teórico no roteiro falta implementar o codigo dessa etapa
    - Implementado o código
    - Resumo geral:
        - Foi a criado a pasta Services adicionado a class ToKenService.cs. 
            - Foi gerado o token usando o metodo GenerateToken com as variaveis tokenHandler, key, tokenDecriptor.
            - No tokenDecriptor foi instanciado SecurityTokenDescriptor e incluido os item Subject, Experires, SigningCredentials.
            - Criado outra variavel token que recebe tokenHandler.CreateToken(tokenDescriptor) a criação do token
            - Que return tokenHandler.WriteToken(token)
        - Na pasta Controllers cria a class AccountController
            - private readonly TokenService _tokenService;
            -  public AccountController(TokenService tokenService)
            {
                _tokenService =  tokenService;
            }
            - [HttpGet("v1/login")]
            - public IActionResult Login([FromServices] TokenService tokenService)
            {
                //var tokenService = new TokenService();
                var token = _tokenService.GenerateToken(new User());
                
                return Ok(token);
            }
        - No program.cs
            - var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
                builder.Services.AddAuthentication(x =>
                {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(x =>
                {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
                };
                });
            - builder.Services.AddTransient<TokenService>();
            - app.UseAuthentication();
            - app.UseAuthorization();
        
        
### Iniciar os testes, proxima aula "TESTANDO AUTENTICAÇÃO E AUTORIZAÇÃO"
    - AccountController.cs
        - Cria 3 metodo get para user, author, admin
        - [HttpGet("v1/user")]
        - public IActionResult GetUser() => Ok(User.Identity.Name)
        - Entender bem autorizado e autenticado. Primeiro vem a autenticação para
            identificar o user depois vem autorização para usar o rota permitida
        - [Authorize(Roles = "user")] => pode fica no ApiController ou no metodo HttpGet("v1/user")
            - Ele verifica se usuario está logado
        - [AllowAnonymous] => metodo HttpGet("v1/user")
    - TokenService.cs
        - inclui o Role, Admin

### Implementado e testado autenticaçao

### Implementar cadastro do usuario
### Cadastrar Usuario
    - ViewModels
        - cria a class RegisterViewModel
            - prop name, email
            - Atributos
                - [Required(ErrorMessage = "Campo obrigatorio")]
                - [EmailAddres(ErrorMessage = "Email inválido")]
    - Controllers
        - AccuntController
            - [HttpPost("v1/accounts/")]
            - public async Task<IActionResult>Post(FromBody RegisterViewModel model, 
                [FromServices]BlogDataContext context){copiar os modelos ja implementado}
            - Slug =  model.Email.Replace("@","-").Replace(".","-")
### Salvando senha no banco de dados
    - Instalar pacote: 
        - dotnet add package SecureIdentity
    - var password = PasswordGenerator.Generate(25);
        user.PasswordHash = PasswordHasher.Hash(password)
    - Revisar a aula REGISTRANDO UM NOVO USUARIO
    - Implementado Account para iserir usario com senha

### Proxima aula autenticacao

    
