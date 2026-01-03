# Crud e Entity Framework
## Crinado projeto
	- dotnet new web -o Blog
## Adiocionando Suporte ao Entity Framework
	- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
	- dotnet add package Microsoft.EntityFrameworkCore.Tools
## Criando banco de dados 
	- Cria pasta Data
	- Cria classe BlogContext.cs
## Iniciando os Controllers
	- Importa pasta Models e Controllers do projeto blog antigo
	- Cria classe HomeController.cs
## Nomeando um endpoint
	- No arquivo Program.cs, adiciona o endpoint padrão
## Configurando a string de conexão
	- No arquivo appsettings.json, adiciona a string de conexão
## Versionamamento do projeto revisar video

## Metodo Async e Await
	- No HomeController.cs, altera os metodos para async e await
	- No BlogContext.cs, altera o metodo OnConfiguring para async e await
## Adicionando Migrations
	- dotnet ef migrations add InitialCreate
	- dotnet ef database update
## Testando o projeto
	- Crud de Categorias

## Testando API
	- Demostra utilicao debug
	- utilizar postman ou similares
## Tratando erros
	- Utilizando try catch nos metodos do HomeController.cs
	- Retornando status code adequado
	- colocar código pra melhor localizar o erro, tipo 05XE9
	- Para pesquisar o codigo no projeto digita Ctrl + F
## Finalizando o Crud de Categorias
	- Testar todos os metodos do Crud de Categorias

