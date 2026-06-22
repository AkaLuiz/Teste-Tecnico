# Cartorio RTDPJ

Sistema simplificado para registro de documentos cartorários, desenvolvido para o teste técnico da Central ON-RTDPJ.

O projeto usa dois servicos backend independentes em C#/.NET, um frontend React + TypeScript e dois bancos PostgreSQL separados.

## Sumario

- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Como rodar com Docker](#como-rodar-com-docker)
- [Como rodar localmente](#como-rodar-localmente)
- [Credenciais de seed](#credenciais-de-seed)
- [Permissões](#permissões)
- [APIs](#apis)
- [Testes](#testes)
- [Decisões técnicas](#decisões-técnicas)
- [O que faria diferente com mais tempo](#o-que-faria-diferente-com-mais-tempo)

## Arquitetura

```text
Frontend React + TypeScript
  |
  | HTTP
  |
  +--> AuthService (.NET)
  |      - Usuarios
  |      - Login
  |      - Emissao de JWT
  |      - Banco: cartorio_auth
  |
  +--> RegistrosService (.NET)
         - CRUD de registros
         - Validacao de JWT
         - Permissoes por papel
         - Regras de status
         - Banco: cartorio_registros
```

Cada servico tem sua própria responsabilidade e seu proprio banco. O `RegistrosService` valida localmente a assinatura do JWT emitido pelo `AuthService`, usando a mesma chave simetrica configurada nos dois servicos.

## Tecnologias

- .NET 10
- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- React
- TypeScript
- Vite
- Bootstrap
- Docker Compose
- xUnit
- Cypress

## Como Rodar Com Docker

Pre-requisitos:

- Docker Desktop instalado e rodando
- Engine Linux do Docker ativo

Na raiz do projeto, rode:

```powershell
docker compose up --build
```

Servicos disponiveis:

```text
Frontend:         http://localhost:5173
AuthService:      http://localhost:5167
RegistrosService: http://localhost:5026
PostgreSQL:       localhost:5432
```

O `docker-compose.yml` sobe:

- `postgres`: banco PostgreSQL
- `auth-service`: API de autenticacao
- `registros-service`: API de registros
- `frontend`: aplicacao React com `npm run dev`

Os bancos criados no container são:

```text
cartorio_auth
cartorio_registros
```

As migrations são aplicadas automaticamente quando os servicos sobem em ambiente `Development`.

Para parar:

```powershell
docker compose down
```

Para apagar tambem os dados do banco:

```powershell
docker compose down -v
```

## Como Rodar Localmente

Pre-requisitos:

- .NET 10 SDK
- Node.js
- PostgreSQL
- Banco PostgreSQL rodando em `localhost:5432`

Crie os bancos:

```sql
CREATE DATABASE cartorio_auth;
CREATE DATABASE cartorio_registros;
```

Atualize os bancos com migrations:

```powershell
dotnet ef database update --project AuthService
dotnet ef database update --project RegistrosService
```

Suba o AuthService:

```powershell
dotnet run --project AuthService
```

Suba o RegistrosService:

```powershell
dotnet run --project RegistrosService
```

Suba o frontend:

```powershell
cd frontend
npm install
npm run dev
```

## Seed

Para que os testes funcionem corretamente, foram criados 3 registros iniciais os quais são 1 de cada tipo.

#### Credenciais De Seed

O `AuthService` possui seed com um usuário de cada papel.

| Papel | Email | Senha |
| --- | --- | --- |
| ADMIN | `admin@example.com` | `admin123` |
| REGISTRADOR | `registrador@example.com` | `registrador123` |
| CONSULTA | `consulta@example.com` | `consulta123` |

As senhas sao armazenadas com hash BCrypt.

## Permissões

### AuthService

| Rota | Acesso |
| --- | --- |
| `POST /auth/login` | Publico |
| `GET /auth/me` | Autenticado |
| `POST /auth/usuarios` | ADMIN |
| `GET /auth/usuarios` | ADMIN |
| `GET /auth/usuarios/{id}` | ADMIN |

### RegistrosService

| Rota | ADMIN | REGISTRADOR | CONSULTA |
| --- | --- | --- | --- |
| `POST /registros` | Sim | Sim | Não |
| `GET /registros` | Sim | Sim | Sim |
| `GET /registros/{id}` | Sim | Sim | Sim |
| `PUT /registros/{id}` | Sim | Sim | Não |
| `PATCH /registros/{id}/status` | Sim | Sim | Não |
| `DELETE /registros/{id}` | Sim | Não | Não |

Sem token ou com token inválido, a API retorna `401`.
Com token válido, mas sem permissão para a ação, a API retorna `403`.

## APIs

As APIs possuem Swagger em ambiente de desenvolvimento.

```text
AuthService Swagger:      http://localhost:5167/swagger
RegistrosService Swagger: http://localhost:5026/swagger
```

### AuthService

#### Login

```http
POST /auth/login
Content-Type: application/json

{
  "email": "admin@example.com",
  "senha": "admin123"
}
```

Resposta:

```json
{
  "token": "jwt"
}
```

#### Usuario Logado

```http
GET /auth/me
Authorization: Bearer {token}
```

#### Criar Usuario

```http
POST /auth/usuarios
Authorization: Bearer {token_admin}
Content-Type: application/json

{
  "name": "Novo Usuario",
  "email": "novo@example.com",
  "senha": "senha123",
  "papel": 1
}
```

Papeis:

```text
0 = Admin
1 = Registrador
2 = Consulta
```

### RegistrosService

Tipos:

```text
0 = Contrato
1 = Procuracao
2 = Notificacao
```

Status:

```text
0 = Pendente
1 = Registrado
2 = Devolvido
```

#### Criar Registro

```http
POST /registros
Authorization: Bearer {token}
Content-Type: application/json

{
  "tipo": 0,
  "nomeApresentante": "Empresa Exemplo",
  "cpfCnpj": "11222333000181",
  "dataEntrada": "2026-06-20",
  "observacoes": "Registro inicial"
}
```

#### Listar Registros

```http
GET /registros?page=1&limit=10&tipo=0&status=0
Authorization: Bearer {token}
```

Filtros:

- `page`: pagina atual
- `limit`: quantidade por pagina
- `tipo`: opcional
- `status`: opcional

#### Buscar Registro Por ID

```http
GET /registros/{id}
Authorization: Bearer {token}
```

#### Atualizar Registro

```http
PUT /registros/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "tipo": 1,
  "nomeApresentante": "Empresa Atualizada",
  "cpfCnpj": "11222333000181",
  "dataEntrada": "2026-06-20",
  "observacoes": "Dados atualizados"
}
```

#### Alterar Status

```http
PATCH /registros/{id}/status
Authorization: Bearer {token}
Content-Type: application/json

{
  "status": 1
}
```

Transições esperadas:

```text
Pendente -> Registrado
Pendente -> Devolvido
Devolvido -> Pendente
Registrado -> qualquer outro status: invalido
```

Transição inválida retorna `422`.

#### Deletar Registro

```http
DELETE /registros/{id}
Authorization: Bearer {token_admin}
```

## Testes

#### Backend

Rodar testes do AuthService:

```powershell
dotnet test AuthService.Tests\AuthService.Tests.csproj
```

Rodar testes do RegistrosService:

```powershell
dotnet test RegistrosService.Tests\RegistrosService.Tests.csproj
```

Também é possível rodar os dois testes de uma vez na raiz do projeto:
```powershell
dotnet test
```

#### Frontend

Instalar dependências e rodar build do frontend:

```powershell
cd frontend
npm install
npm run dev
```

Executar testes E2E com Cypress:
```powershell
npm test
```

Coberturas implementadas:

- Login com usuario seed
- Endpoint `/auth/me`
- Validação de CPF
- Validação de CNPJ
- Regras principais de transição de status

## Decisões Técnicas

### JWT validado localmente

O `RegistrosService` valida localmente o JWT emitido pelo `AuthService`. Essa escolha reduz acoplamento em tempo de execucao e evita uma chamada HTTP ao `AuthService` em toda requisicao protegida.

## O Que Faria Diferente Com Mais Tempo

- Extrair a configuracao do JWT para variaveis de ambiente ou secret manager.
- Criar um BFF/API Gateway para centralizar as chamadas do frontend.
- Implementar refresh token.
- Adicionar CI no GitHub Actions para rodar build, lint e testes a cada push.
- Melhorar a paginação retornando tambem total de itens e total de paginas.
