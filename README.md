# 🔐 UsuariosApi - ASP.NET Core 8 & Identity Framework

API RESTful desenvolvida em .NET 8 focada na implementação de controle de acesso, cadastro e autenticação segura de usuários utilizando o ecossistema oficial do ASP.NET Core Identity com persistência em banco de dados MySQL.

---

## 🎯 Objetivos do Projeto

- Gerenciamento de Identidade: Cadastro e autenticação de usuários com suporte a atributos personalizados (ex: DataNascimento).
- Segurança da Informação: Armazenamento seguro de credenciais com geração automática de hash de senhas via UserManager.
- Proteção de Segredos: Isolamento de credenciais de banco de dados no ambiente local utilizando User Secrets (dotnet user-secrets), evitando o vazamento de senhas em controle de versão.
- Persistência de Dados: Mapeamento e gestão das tabelas nativas do Identity através do IdentityDbContext e Entity Framework Core.

---

## 🛠️ Tecnologias e Pacotes

- .NET 8.0 (ASP.NET Core Web API)
- Entity Framework Core 8.0
- ASP.NET Core Identity 8.0
- Pomelo Entity Framework Core MySQL 8.0
- AutoMapper 13.0

---

## 🏗️ Arquitetura e Estrutura

- Models/Usuario.cs: Modelo customizado que herda de IdentityUser para estender propriedades do usuário.
- Data/UsuarioDbContext.cs: Contexto do banco que herda de IdentityDbContext<Usuario> para gerenciamento automatizado de tabelas de segurança.
- Data/Dtos/: Objetos de transferência de dados com validações de atributos ([Required], [Compare], [DataType]).
- Profiles/: Mapeamento de objetos com AutoMapper.

---

## 🛡️ Segurança Local (User Secrets)

As credenciais do banco de dados não são expostas no arquivo appsettings.json. Para configurar a string de conexão em ambiente de desenvolvimento local, utilize:

dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:UsuariosConnection" "server=localhost;port=3306;database=Usuarios;user=seu_usuario;password=sua_senha"
