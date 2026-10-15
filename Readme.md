MedConnect - Camada de Domínio e Persistência DAO (C#)

Projeto desenvolvido como parte do PIM (Projeto Integrado Multidisciplinar), contemplando os requisitos das disciplinas de Programação Orientada a Objetos II, Desenvolvimento de Software para a Internet e Tópicos Especiais de Programação Orientada a Objetos.

Este repositório contém a implementação da camada de domínio (entidades de negócio) e o mecanismo de acesso a dados baseado em padrão DAO (PessoaDAO) em linguagem C# (.NET).

🏗️ Arquitetura do Projeto

O sistema foi estruturado seguindo o princípio de separação de responsabilidades em camadas lógicas:

Dominio: Contém as classes de entidade que modelam o negócio conforme o Diagrama de Classes (Pessoa, Endereco, Telefone e TipoTelefone).

Persistencia: Contém a classe utilitária de escopo estático PessoaDAO, responsável por encapsular as operações de persistência e interagir com o banco de dados relacional (CRUD completo).

📂 Estrutura de Pastas

MedConnect/
│
└── MedConnect.Core/
    ├── Dominio/
    │   ├── Pessoa.cs
    │   ├── Endereco.cs
    │   ├── Telefone.cs
    │   └── TipoTelefone.cs
    │
    ├── Persistencia/
    │   └── PessoaDAO.cs
    │
    ├── Program.cs
    └── MedConnect.Core.csproj


📋 Pré-requisitos

Para compilar e testar o projeto em sua máquina, certifique-se de possuir:

.NET SDK instalado (versão 6.0 ou superior recomendada).

Um editor de código ou IDE como o Visual Studio Code ou Visual Studio.

(Opcional) Microsoft SQL Server caso deseje testar a persistência real com banco de dados.

🚀 Como Executar o Projeto

Clone ou abra a pasta do projeto no terminal de sua preferência.

Navegue até o diretório do projeto onde se encontra o arquivo .csproj:

cd MedConnect.Core


Instale a dependência do driver do SQL Server via NuGet:

dotnet add package System.Data.SqlClient


Compile o projeto para verificar se não há erros de sintaxe:

dotnet build


Execute a aplicação de teste (Program.cs):

dotnet run


🛠️ Métodos Disponíveis na PessoaDAO

A classe PessoaDAO expõe métodos estáticos de escopo global para o gerenciamento das entidades:

Inserir(Pessoa pessoa): Realiza a inserção transacional da pessoa e suas respectivas dependências.

BuscarPorCpf(long cpf): Localiza e retorna uma instância de Pessoa com base no CPF informado.

Atualizar(Pessoa pessoa): Atualiza os dados cadastrais da pessoa no banco.

Excluir(long id): Remove o registro e suas dependências de forma segura.