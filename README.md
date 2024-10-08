# ToDoList API com Autenticação JWT

## Descrição

Esta é uma API de gerenciamento de tarefas desenvolvida com **ASP.NET Core 6**. A API permite que os usuários criem, editem, deletem e visualizem tarefas. A autenticação é realizada por meio de tokens JWT (JSON Web Token), garantindo que apenas usuários autenticados possam acessar as funcionalidades da API.

## Funcionalidades

- **Autenticação JWT**: Proteção das rotas com autenticação.
- **CRUD de Tarefas**:
  - Criar uma nova tarefa.
  - Atualizar uma tarefa existente.
  - Deletar uma tarefa.
  - Obter uma lista de todas as tarefas.
  - Buscar tarefa por ID, título ou status.
- **Swagger/OpenAPI**: Documentação da API acessível via Swagger UI.

## Tecnologias Utilizadas

- **ASP.NET Core 6**
- **Entity Framework Core**
- **SQL Server**
- **JWT (JSON Web Token)** para autenticação
- **Swagger/OpenAPI** para documentação

## Pré-requisitos

Antes de começar, você precisará ter o seguinte instalado em sua máquina:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Postman](https://www.postman.com/) ou similar para testar a API

## Instalação e Configuração

### 1. Clone o Repositório

git clone https://github.com/larissabpaz/ToDoListProjectJWT.git

### 2. Configure o Banco de Dados
Altere a string de conexão no arquivo appsettings.json para apontar para o seu banco de dados SQL Server:

json
Copiar código
{
  "ConnectionStrings": {
    "ConexaoPadrao": "Server=SEU_SERVIDOR;Database=NOME_DO_BANCO;User Id=SEU_USUARIO;Password=SUA_SENHA;"
  },
  "Jwt": {
    "Key": "sua-chave-secreta-jwt",
    "Issuer": "seu-emissor-jwt"
  }
}

### 3. Execute as Migrações
Após configurar a string de conexão, execute as migrações para criar o banco de dados:

bash
Copiar código
dotnet ef database update

### 4. Execute o Projeto
Inicie a aplicação:

bash
Copiar código
dotnet run

### 5. Acesse a Documentação do Swagger
Após iniciar a aplicação, você pode acessar a documentação da API via Swagger na seguinte URL:

bash
Copiar código
https://localhost:{PORTA}/swagger/index.html
Substitua {PORTA} pela porta correta onde a aplicação está rodando.

## Endpoints Principais
Autenticação
POST /auth/login: Autenticação do usuário com JWT.
Tarefas
GET /task/ObterTodos: Obter todas as tarefas.
GET /task/{id}: Obter uma tarefa por ID.
POST /task: Criar uma nova tarefa.
PUT /task/{id}: Atualizar uma tarefa existente.
DELETE /task/{id}: Deletar uma tarefa.
Exemplo de Requisição de Login
http
Copiar código
POST /auth/login
Content-Type: application/json

{
  "username": "seu-usuario",
  "password": "sua-senha"
}
Exemplo de Requisição de Criação de Tarefa
http
Copiar código
POST /task
Content-Type: application/json
Authorization: Bearer {seu-token-jwt}

{
  "title": "Nova Tarefa",
  "description": "Descrição da nova tarefa",
  "status": 0
}

## Executando com Docker (Opcional)
### 1. Criar a Imagem Docker
Você pode criar uma imagem Docker para rodar a API:

bash
Copiar código
docker build -t todolist-api-jwt .
### 2. Executar o Container
Após construir a imagem, execute o container:

bash
Copiar código
docker run -d -p 8080:80 todolist-api-jwt
Agora você pode acessar a API em http://localhost:8080.

### Melhorias Futuras
Implementar controle de usuários.
Implementar categorias para as tarefas.
Melhorar os filtros de busca.

### Contribuição
Sinta-se à vontade para contribuir com este projeto. Para isso, siga os passos abaixo:

Faça um fork do projeto.
Crie uma nova branch (git checkout -b feature/nova-funcionalidade).
Commit suas alterações (git commit -m 'Adiciona nova funcionalidade').
Envie para a branch principal (git push origin feature/nova-funcionalidade).
Abra um Pull Request.