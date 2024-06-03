# Teste para Zdz

Esta é a solução para o teste, que envolve a adição de produções e pedidos. Estou confiante de que essa experiência fortaleceu minhas habilidades e me preparou para enfrentar desafios ainda maiores no desenvolvimento de software. Meu conhecimento do Vue é limitado, pois ainda não tinha trabalhado em um projeto com Vue antes. Portanto, meu foco foi em garantir que o backend estivesse excelente. No entanto, isso não significa que o frontend foi feito de qualquer jeito.

## 1 - Back End

Foi construído com o intuito de ser escalável e modular, dividindo-o em quatro dependências principais, cada uma com sua função específica. Essa estrutura permite uma manutenção mais fácil e futuras expansões. Abaixo está a estrutura detalhada:

- `ESTRUTURA`

  - `1- Core:` Camada mais baixa do projeto. Não consegue acessar suas camadas superiores. Nesta camada estão os modelos de dados e as regras de negócios definidas. Você pode ver o modelo dentro de BackEnd `zdz.drawio.png`

  . tambem gaursda as excercoes de error dos modelos

  - `2- UseCase:` Módulo responsável por definir as interfaces que serão utilizadas na implementação dos casos de uso. O único módulo que pode acessar, é o `Core`.

  - `3- Application:` Responsável por implementar os casos de uso e criar os gateways que esses casos de uso vão utilizar. Pode acessar os módulos `Core` e `UseCas`.

  - `4- Infrastructure:` Responsável por implementar a API para a execução do software. Pode acessar os `módulos` `Core`, `UseCase` e `Application`.

## 2- Front End

Front-end onde o usuário pode criar um produto e um pedido para esse produto. Também pode deletar o produto ou o pedido, além de atualizar ambos.

## 3- Como Rodar O Projeto

O projeto foi planejado para rodar 100% em contêiner. Portanto, se você não tiver o Docker, será necessário alterar a porta da URL de acordo com a porta em que o backend está rodando.

Para isso, altere a porta em `nuxt.config.ts`:

```bash
publicRuntimeConfig: {
    url_base: "http://localhost:{sua porta}/api/v1",
  },
```

Além disso, será necessário alterar o `appsettings.json` dentro de `Infrastructure` e configurar os parâmetros de conexão do seu banco Postgres da sua máquina ou servidor escolhido.

Se você tiver o Docker instalado, basta rodar o comando abaixo na raiz do projeto:

```bash
docker-compose up -d
```

O front-end estará disponível em http://localhost:3000 e o back-end em http://localhost:5042.

Caso queira ver a documentação das rotas, acesse http://localhost:5042/swagger/index.html.

Se você rodou o projeto manualmente seguindo os passos anteriores, apenas altere a porta para a porta selecionada da sua aplicação!
