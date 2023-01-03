
### Processo de desenvolvimento

* Não sabia oque era Scraping então pesquisei e entendi o conceito <br>
* Pesquisei em como aplicar com .net <br>
* Testei uma biblioteca de Scraping estática mas vi que não era o caso e precisaria usar Selenium <br>
* Criei um projeto descartável para testar Scraping com Selenium, gastei um tempo testando e desenvolvendo o algorítimo e consegui o resultado que queria <br>
* Iniciei o projeto .net web api <br>
* Criei o Model + Controller e colei o código de Scraping nele, testei e funcionou <br>
* Criei o banco mongoDB no Atlas, adicionei o json e model de configuração <br>
* Adicionei o Service que interage com o banco e configurei as injeções de dependência <br>
* Criei uma Service para o Scraping e extrai o código do Controller <br>
* Cometi o deslize de juntar muitos arquivos para commitar e também não havia criado o gitignore <br>
* Criei o gitignore e Commitei todos os aquivos no Github <br>
* Pesquisei sobre como automatizar o processo de Scraping e descobri oque era CRON <br>
* Descobri a lib Quatz.Net que ajuda a implementar o Job e configurar o Trigger, testei e funcionou <br>
* Transferi o código de Scraping que anteriormente era ativado por uma Controller para o Job e deletei o Controller <br>
* Implementei a paginação no endpoint de Produtos 
* Refatorei o código com uma atenção especial no ScrapingService <br>

Obs. não consegui implementar o passo "Para gerar a URL das imagens, revisar o How to do projeto em: https://wiki.openfoodfacts.org/Developer-How_To",
a, os links da documentação me respondem erro 403.

![erro403_doc_imagens_url](https://github.com/BrunoUmbelino/Scraping-Project/blob/main/Product%20Scraping/403.png)


### Como usar

###### Requisito: possuir o Microsoft .Net SDK 6

1_Clone o repositório e vá até a pasta do projeto

    git clone https://github.com/BrunoUmbelino/Scraping-Project.git
    cd '.\Scraping-Project\Product Scraping\'

2_Compile e execute o App

    dotnet build
    dotnet run

3_Abra o Swagger em seu navegador

    https://localhost:7144/swagger/index.html
