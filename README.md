# Cartorio RTDPJ

Sistema simplificado para registro de documentos cartorarios, desenvolvido para o teste tecnico da Central ON-RTDPJ.

O projeto usa dois servicos backend independentes em C#/.NET, um frontend React + TypeScript e dois bancos PostgreSQL separados.

## Sumario

- [Arquitetura](#arquitetura)
- [Tecnologias](#tecnologias)
- [Como rodar com Docker](#como-rodar-com-docker)
- [Como rodar localmente](#como-rodar-localmente)
- [Credenciais de seed](#credenciais-de-seed)
- [Permissoes](#permissoes)
- [APIs](#apis)
- [Testes](#testes)
- [Decisoes tecnicas](#decisoes-tecnicas)
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

Cada servico tem sua propria responsabilidade e seu proprio banco. O `RegistrosService` valida localmente a assinatura do JWT emitido pelo `AuthService`, usando a mesma chave simetrica configurada nos dois servicos.

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

Os bancos criados no container sao:

```text
cartorio_auth
cartorio_registros
```

As migrations sao aplicadas automaticamente quando os servicos sobem em ambiente `Development`.

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

## Credenciais De Seed

O `AuthService` possui seed com um usuario de cada papel.

| Papel | Email | Senha |
| --- | --- | --- |
| ADMIN | `admin@example.com` | `admin123` |
| REGISTRADOR | `registrador@example.com` | `registrador123` |
| CONSULTA | `consulta@example.com` | `consulta123` |

As senhas sao armazenadas com hash BCrypt.

## Permissoes

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
| `POST /registros` | Sim | Sim | Nao |
| `GET /registros` | Sim | Sim | Sim |
| `GET /registros/{id}` | Sim | Sim | Sim |
| `PUT /registros/{id}` | Sim | Sim | Nao |
| `PATCH /registros/{id}/status` | Sim | Sim | Nao |
| `DELETE /registros/{id}` | Sim | Nao | Nao |

Sem token ou com token invalido, a API retorna `401`.
Com token valido, mas sem permissao para a acao, a API retorna `403`.

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

Transicoes esperadas:

```text
Pendente -> Registrado
Pendente -> Devolvido
Devolvido -> Pendente
Registrado -> qualquer outro status: invalido
```

Transicao invalida retorna `422`.

#### Deletar Registro

```http
DELETE /registros/{id}
Authorization: Bearer {token_admin}
```

## Testes

Rodar testes do AuthService:

```powershell
dotnet test AuthService.Tests\AuthService.Tests.csproj
```

Rodar testes do RegistrosService:

```powershell
dotnet test RegistrosService.Tests\RegistrosService.Tests.csproj
```

Rodar build do frontend:

```powershell
cd frontend
npm run build
```

Coberturas implementadas:

- Login com usuario seed
- Endpoint `/auth/me`
- Validacao de CPF
- Validacao de CNPJ
- Regras principais de transicao de status

## Decisoes Tecnicas

### Dois bancos separados

O `AuthService` e o `RegistrosService` usam bancos diferentes para preservar a fronteira entre os servicos. O servico de registros armazena o identificador do usuario que criou o registro, mas nao acessa diretamente as tabelas do banco de autenticacao.

### JWT validado localmente

O `RegistrosService` valida localmente o JWT emitido pelo `AuthService`. Essa escolha reduz acoplamento em tempo de execucao e evita uma chamada HTTP ao `AuthService` em toda requisicao protegida.

### Controle de permissao no backend e no frontend

O backend usa `[Authorize]` e `[Authorize(Roles = "...")]` como camada principal de seguranca. O frontend tambem esconde acoes que o papel atual nao pode executar, melhorando a experiencia do usuario sem substituir a autorizacao do backend.

### Docker para ambiente completo

O Docker Compose foi usado para subir frontend, dois servicos backend e PostgreSQL com um unico comando, facilitando a avaliacao e reduzindo dependencia de configuracoes locais.

## O Que Faria Diferente Com Mais Tempo

- Criar testes de integracao cobrindo o fluxo completo: login, criar registro, listar, alterar status e validar uma acao proibida com `403`.
- Adicionar testes automatizados no frontend com Testing Library ou Playwright.
- Extrair a configuracao do JWT para variaveis de ambiente ou secret manager.
- Criar um BFF/API Gateway para centralizar as chamadas do frontend.
- Melhorar o tratamento global de erros para padronizar respostas JSON.
- Adicionar CI no GitHub Actions para rodar build, lint e testes a cada push.
- Melhorar a paginacao retornando tambem total de itens e total de paginas.
