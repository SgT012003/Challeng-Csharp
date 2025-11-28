# Care Plus API

## Visão Geral

Este documento fornece uma análise técnica detalhada da **Care Plus API**, uma plataforma de gamificação de bem-estar desenvolvida em **ASP.NET Core 8.0**. A API foi projetada com uma arquitetura robusta e escalável, seguindo os princípios **SOLID** e as melhores práticas de design de API **RESTful**.

O objetivo principal da API é incentivar hábitos saudáveis por meio de desafios, recompensas e um sistema de ranking, promovendo o engajamento do usuário e o bem-estar.

## Group
|Name|RM|
|:-:|:-:|
|Diogo Julio|553837|
|Jonata Rafael|552939|
|Matheus Zottis|94119|
|Victor Didoff|552965|
|Vinicius da Silva|553240|

## 🛠️ Tecnologias e Arquitetura

A API foi construída utilizando um conjunto de tecnologias modernas e comprovadas, garantindo desempenho, segurança e manutenibilidade.

| Categoria | Tecnologia/Padrão | Descrição |
| :--- | :--- | :--- |
| **Framework** | ASP.NET Core 8.0 | Framework de alta performance para construção de APIs web. |
| **Linguagem** | C# 12 | Linguagem de programação principal do projeto. |
| **Banco de Dados** | SQL Server | Sistema de gerenciamento de banco de dados relacional. |
| **ORM** | Entity Framework Core 8.0 | Framework de mapeamento objeto-relacional para acesso a dados. |
| **Containerização** | Docker & Docker Compose | Orquestração de contêineres para garantir um ambiente de desenvolvimento e produção consistente. |
| **Mapeamento** | AutoMapper | Biblioteca para mapeamento de objetos entre camadas (e.g., Modelos para DTOs). |
| **Documentação** | Swagger (OpenAPI) | Geração de documentação interativa para a API. |

### Arquitetura em Camadas e SOLID

O projeto adota uma arquitetura em camadas (n-tier) para promover a separação de responsabilidades (SoC) e o baixo acoplamento, alinhando-se diretamente com os princípios SOLID.

| Camada | Diretório | Responsabilidade | Princípio SOLID Aplicado |
| :--- | :--- | :--- | :--- |
| **Apresentação** | `Controllers` | Expor os endpoints da API, receber requisições HTTP e retornar respostas. | **SRP (Single Responsibility Principle):** Cada controller é responsável por um recurso específico. |
| **Serviços** | `Services` | Orquestrar as regras de negócio e a lógica da aplicação. | **SRP, OCP (Open/Closed Principle):** Os serviços encapsulam a lógica de negócio, e são abertos para extensão (novas funcionalidades) e fechados para modificação. |
| **Acesso a Dados** | `Data/Repositories` | Abstrair o acesso ao banco de dados, implementando o padrão Repositório. | **SRP, LSP (Liskov Substitution Principle):** Os repositórios são substituíveis e possuem uma única responsabilidade. |
| **Contratos** | `Interfaces` | Definir os contratos (interfaces) para os serviços e repositórios. | **DIP (Dependency Inversion Principle):** As camadas de alto nível (serviços) dependem de abstrações (interfaces), não de implementações concretas. |
| **Modelos** | `Models` | Representar as entidades do domínio e do banco de dados. | **SRP:** Cada modelo representa uma única entidade. |
| **DTOs** | `DTOs` | Transferir dados entre as camadas, especialmente entre a API e os clientes. | **SRP:** Cada DTO é projetado para uma operação específica (e.g., criação, visualização). |

## 🚀 Como Executar o Projeto

O projeto é containerizado e pode ser executado facilmente com o Docker e o Docker Compose.

### Pré-requisitos

*   Docker
*   Docker Compose

### 1. Execução com Docker Compose

Na raiz do diretório `Challeng-Csharp/Challeng-Csharp`, execute o seguinte comando para construir a imagem da API, iniciar os contêineres (API e banco de dados) e aplicar as migrações do Entity Framework Core:

```bash
docker-compose up --build
```

A API estará disponível em `http://localhost:8080`.

### 2. Migrações e Dados Iniciais

As migrações do Entity Framework Core são aplicadas automaticamente na inicialização do contêiner da API. O `DataSeeder` é responsável por popular o banco de dados com dados iniciais para testes e demonstração.

## 🔗 Endpoints da API (RESTful)

A API segue os princípios RESTful, utilizando os verbos HTTP corretamente, retornando os códigos de status apropriados e utilizando uma estrutura de URL baseada em recursos.

A documentação interativa do Swagger está disponível em:

**URL:** `http://localhost:8080/swagger/index.html`

### Principais Recursos

*   **/api/Benefits**: Gerencia os benefícios (recompensas) e o resgate pelos usuários.
*   **/api/Challenges**: Gerencia os desafios disponíveis e a participação dos usuários.
*   **/api/Ranking**: Fornece o ranking dos usuários com base em sua performance.

### Exemplo de Requisição: Listar Benefícios de um Usuário

**URL:** `GET http://localhost:8080/api/Benefits?userId={userId}`

**Resposta (200 OK):**

```json
[
  {
    "rewardId": "c1b2a3d4-e5f6-7890-1234-567890abcdef",
    "name": "Voucher de R$50 em Loja de Esportes",
    "description": "Use este voucher para comprar equipamentos esportivos.",
    "pointsCost": 500,
    "isClaimed": true
  },
  {
    "rewardId": "d2c3b4a5-f6e7-8901-2345-67890abcdef1",
    "name": "Consulta com Nutricionista",
    "description": "Agende uma consulta online com um de nossos nutricionistas parceiros.",
    "pointsCost": 1000,
    "isClaimed": false
  }
]
```

## 📐 Análise de Design e Princípios

### RESTful API

A API foi projetada para ser RESTful, o que significa:

*   **Stateless:** Cada requisição do cliente para o servidor deve conter todas as informações necessárias para entender e processar a requisição. O servidor não armazena nenhum estado do cliente.
*   **Cliente-Servidor:** A arquitetura é baseada na separação entre o cliente (que consome a API) e o servidor (que a fornece), permitindo que evoluam de forma independente.
*   **Interface Uniforme:** A utilização de URIs para identificar recursos, o uso de verbos HTTP para definir ações e o retorno de códigos de status padronizados garantem uma interface consistente e previsível.

### SOLID

Os cinco princípios SOLID são a base da arquitetura do software, resultando em um código mais limpo, modular e fácil de manter:

*   **S (Single Responsibility Principle):** Cada classe e método tem uma única responsabilidade. Por exemplo, o `ChallengeService` é responsável apenas pela lógica de negócio dos desafios.
*   **O (Open/Closed Principle):** O software é aberto para extensão, mas fechado para modificação. Novas funcionalidades podem ser adicionadas sem alterar o código existente, por exemplo, adicionando um novo tipo de desafio.
*   **L (Liskov Substitution Principle):** Os objetos de uma classe derivada devem ser capazes de substituir os objetos da classe base sem afetar a corretude do programa. Isso é garantido pelo uso de interfaces e herança de forma apropriada.
*   **I (Interface Segregation Principle):** As interfaces são segregadas por cliente, de modo que as classes não precisem implementar métodos que não utilizam. Por exemplo, a interface `IRepository<T>` define operações genéricas, enquanto `IChallengeRepository` adiciona métodos específicos para desafios.
*   **D (Dependency Inversion Principle):** As dependências são invertidas por meio do uso de injeção de dependência. As classes de alto nível dependem de abstrações (interfaces), e não de implementações concretas, o que desacopla o código e facilita os testes.

## Observações Finais

Este projeto demonstra a aplicação de padrões de design e arquitetura de software modernos para a construção de uma API robusta e escalável com ASP.NET Core. A combinação de uma arquitetura em camadas, os princípios SOLID e o design RESTful resulta em uma base sólida para o desenvolvimento de novas funcionalidades e a manutenção a longo prazo.
