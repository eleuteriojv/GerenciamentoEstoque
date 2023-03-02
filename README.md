# Sistema de Gerenciamento de Estoque

O projeto proposto consiste em:

- Uma API com a permissão de criar, atualizar, excluir e visualizar listagem de produtos, lojas e itens no estoque.

- Cadastro de informações de produtos, podendo criar, atualizar, excluir e visualizar listagem.

- Cadastro de informações de lojas, podendo criar, atualizar, excluir e visualizar listagem.

- Associação de uma loja a um produto, compondo um estoque.

Foram desenvolvidos dois projetos: Uma API que realiza todo processo de inserção, exclusão, busca e atualização de dados e um projeto Web que provém
uma interface adequada para um gerenciamento de dados, com listas contendo uma opção para buscar dados de maneira dinâmica e além disso, 
opções para outras telas para edição, exclusão, atualização e visualização específica de um registro. 

Para rodar a solução, é necessário configurar no Visual Studio o debug simultâneo do projeto Web e do projeto API, seguindo o caminho abaixo:

- Após abrir a solução do projeto, clique em "Projeto" e depois em "Definir Projeto de Inicialização"
![Projeto 1](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/DefinirProjetoInicializa%C3%A7%C3%A3o.jpeg)

- Selecione "Vários projetos de inicialização" e depois em "Ação" trocar de "nenhum" para "iniciar em ambos", conforme a imagem abaixo.
![Projeto 2](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/SelecionarProjetos.jpeg)

- Após realizar essa configuração, será necessário realizar um update para o SQL Server local (Arquivos de migração já se encontram presentes na solução), digitando no console de Gerenciador de Pacotes o comando Update-Database.

![DataBase](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/UpdateDataBase.jpeg)

### Para acessar API, a autenticação é feita da seguinte forma:

- Acessar o endpoint "Account", depois clicar em "Try Out" e então, digitar as informações necessárias para gerar o token (Username, Password, Role):
![TryOut](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/AcessoAPI.jpeg)
- Após realizar essa etapa, copie o token gerado:
![Token](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/CopiarTokenGerado.jpeg)
- Clique em "Authorize" no inicio da página:
![Autorizar](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/CliqueAuthorize.jpeg)
- Digite na caixa de texto "Value" o valor "Bearer" acompanhado do token gerado, conforme a imagem abaixo:
![Autorizar](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/BearerToken.jpeg)
- Autorização finalizada. 

### Para acessar o projeto Web, precisará do "Nome de Usuário" e "Senha":

Nome de Usuário: "admin"

Senha: "admin"

![AcessoWeb](https://github.com/eleuteriojv/GerenciamentoEstoque/blob/master/GerenciamentoEstoque.Web/wwwroot/img/UsuarioSenhaWeb.jpeg)

Após realizar essas etapas, o sistema funcionará corretamente. 

### Contato

-E-mail: joaovr2012@outlook.com

-LinkedIn: https://www.linkedin.com/in/joaovitoreleuterio/
