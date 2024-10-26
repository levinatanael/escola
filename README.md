# 🌐 Projeto de Estudos - Gestão Escolar

Projeto fictício para gestão de escola com o objetivo de explorar as tecnlogias das quais eu possuo domínio.

![Badge em Desenvolvimento](http://img.shields.io/static/v1?label=STATUS&message=EM%20DESENVOLVIMENTO&color=GREEN&style=for-the-badge)

# :hammer: Funcionalidades do projeto

- [X] `Gestão de Alunos`: CRUD do cadastro de alunos
- [ ] `Gestão de Matrículas`
- [ ] `Gestão de Notas`
- [ ] `Gestão de Classes`
- [ ] `Gestão de Professores`
- [ ] `Gestão de Aulas`
- [ ] `Gestão de Faltas`
- [ ] ...

## ✔️ Técnicas e tecnologias utilizadas

- ``WebAPI .NET C#``
- ``Entity Framework Core``
- ``Injeção de Dependência (Ioc)``
- ``AutoMapper``
- ``Swagger``
- ``Docker``
- ``Autenticação JWT``
- ``Testes Unitários``
- ``SOLID (separação de responsabilidades e baixo acoplamento)``
- ``Migrations``
- ...

## 📁 Acesso ao projeto
Você pode acessar os arquivos do projeto clicando [aqui](https://github.com/levinatanael/escola).

## 🪒 Executar o projeto

- ```dotnet build```
- ```dotnet ef migrations add InitialCreate --project Escola.Infra --startup-project Escola.WebAPI```
- ```dotnet ef database update --project Escola.Infra --startup-project Escola.WebAPI```

