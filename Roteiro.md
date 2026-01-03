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
### Configurando a string de conexão
	- No arquivo appsettings.json, adiciona a string de conexão
### Iniciando os Controllers
	- Program.cs
		- Adiciona o endpoint padrão

