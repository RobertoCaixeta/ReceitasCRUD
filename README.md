# 🍲 ReceitasCRUD

Um projeto ASP.NET Core que oferece uma API para gerenciar receitas e ingredientes. Ideal para aprender como construir uma API RESTful utilizando .NET 8 com EF Core e SQLite.

---

## 📦 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core (Minimal API)
- Entity Framework Core
- SQLite
- Swagger (Swashbuckle)

---

## 🚀 Como Rodar o Projeto Localmente

### 1. Clone o repositório

```bash
git clone https://github.com/RobertoCaixeta/ReceitasCRUD.git
cd ReceitasCRUD
```
### 2. Restaure os pacotes
```bash
dotnet restore
```

### 3. Configure o banco de dados
Certifique que no arquivo Data/Context.cs a string de conexão esteja apontando para um arquivo válido.

### 4. Execute as migrations
Caso queira criar uma nova Migration
```bash
dotnet ef migrations add NomeMigration
```
ou 

```bash
dotnet ef database update
```
Caso queira utilizar a já criada.

### 5. Rode o projeto
Execute o projeto com:
```bash
dotnet run
```
Abra seu navegador em:
```bash
https://localhost:5010/swagger
```
Ou em alguma outra porta de preferência específicada em:
ReceitasCRUD.http
Properties\launchSettings.json

Com isso será possível testar os seguintes endpoints:

### 🧾 Receitas

- **GET** `/receita`  
  Retorna a lista de receitas com seus respectivos ingredientes.

- **GET** `/receita/{id}`  
  Retorna os dados de uma receita específica pelo seu ID.

- **POST** `/receita`  
  Cria uma nova receita.  

  **PUT** `/receita/{id}`  
  Atualiza os dados de uma receita específica pelo seu ID.

  **DELETE** `/receita/{id}`  
  Exclui uma receita específica pelo seu ID.

### 🧾 Ingredientes

- **GET** `/ingrediente`  
  Retorna a lista de ingredientes..

- **GET** `/ingrediente/{id}`  
  Retorna os dados de ums ingrediente específico pelo seu ID.

- **POST** `/ingrediente`  
  Cria um novo ingrediente.  

  **PUT** `/ingrediente/{id}`  
  Atualiza os dados de um ingrediente específico pelo seu ID.

  **DELETE** `/ingrediente/{id}`  
  Exclui um ingrediente específico pelo seu ID.
  

