# Scraping Project (Backend Challenge 20220626)

Este projeto é a implementa de um desafio da coodesh, ele realiza o Web Scraping automatizado do portal "Open Food Facts", salva os dados em um banco e disponibiliza através de uma Web API.

### Rotas

``` [GET] "/" ```
 Devolve uma string com o nome do teste. 

``` [GET] "/Products/?indexPage=nº ```
 Devolve todos os produtos limitados pela página correspondente ao parámetro, cada página possui no máximo 10 items. 

``` [GET] "/Products/{code}" ```
 Devolve o produto que corresponde ao "code" informado via parámetro.

### Tecnologias

* ASP.NET Core Web API (.Net Framework 6)
* C# 
* Quartz.Net (Jobs em CRON)
* Selenium + Google Chrome
* MongoDB oficcial Driver
* Cloud MongoDB Atlas
* Swagger API


### Como usar

#### Build Manual

###### Requisito: possuir o Microsoft .Net SDK 6
1_Clone o repositório e vá até a pasta do projeto

    git clone https://github.com/BrunoUmbelino/Scraping-Project.git
    cd '.\Scraping-Project\Product Scraping\'

2_Compile e execute o App

    dotnet build
    dotnet run

3_Abra o Swagger em seu navegador

    https://localhost:7144/swagger/index.html


#### Build via Docker

###### Requisito: possuir o Docker
1_Baixe a imagem do projeto e execute o container

    docker run -it --rm --name web-scraping-container -p 5000:80 -p 5002:443 brunoumbelino/web-scraping
    
Obs. Swagger não implementado no Container 


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
* Estudei/revisei sobre Docker e implementei a Dockerização do app em modo de produção <br>

Obs. não consegui implementar o passo "Para gerar a URL das imagens, revisar o How to do projeto em: https://wiki.openfoodfacts.org/Developer-How_To",
a, os links da documentação me respondem erro 403.

![erro403_doc_imagens_url](https://github.com/BrunoUmbelino/Scraping-Project/blob/main/Product%20Scraping/403.png)
