<h2> ✈ Aeroporto OnTheFly  ✈  </h2>

Aplicação voltada com a finalidade de interagir informações entre APIs de projetos diferentes dentro de uma mesma solução.
Projeto de um aeroporto que controla o cadastro de passageiros, companhias, aeronaves, voo e vendas apenas para pessoas físicas.
Objetivo Específico
Apresentar informações sobre um projeto que controlará as vendas de passagens áreas de um aeroporto denominado ON THE FLY. Os módulos são referentes aos controles de:<br>

⦁	Passageiros; <br>
⦁	Companhias Aéreas; <br>
⦁	Aeronaves;<br>
⦁	Voos;<br>
⦁	Venda de passagens;<br>

O aeroporto atende apenas pessoas físicas.

- Passageiros:
Cada passageiro vai conter os seguintes dados: CPF,  Nome, Gênero, Telefone, Data de aniversario, Data de Registro, Status e endereço.
Passageiros menores de 18 anos, podem ser cadastrados, mas não podem comprar nenhuma passagem.
- Companhias:
Somente serão aceitos cadastros de pessoas jurídicas. Companhias podem ser cadastradas e devem conter ao menos 1(uma) aeronave no sistema.
A companhia pode ser cadastrada mesmo com o CNPJ restrito.
- Endereço:
Para passageiros e companhias, irá consultar um endereçoi via API do VIACEP a partir do CEP que será informado.
- Aeronave:
As aeronaves deverão ser cadastradas a partir de uma companhia existente, mesmo se aquela companhia tiver CNPJ restrito.
Somente a capacidade da aeronave poderá ser alterada após o cadastro.
- Voos:
Cadastros dos voos e passagens disponibilizados pelas companhias aéreas atendidas pelo aeroporto. Poderão ser operados, depois de cadastrado, somente para cancelamento do voo. Os destinos possíveis deverão ser consultados por uma API, possibilitando somente voos nacionais. Nenhuma companhia poderá cadastrar voo quando tiver o cadastro de restrição positivo.
- Vendas:
Após o cadastro de voos, as passagens ficam disponíveis para serem vendidas de acordo com o máximo de assentos definida pela capacidade do avião. A venda não poderá constar o mesmo CPF na lista de passageiros. Cadastro das passagens reservadas ou vendidas de cada voo disponível. Nenhum passageiro na lista de vendas de passagem pode constar na lista de restritos. Caso aconteça a venda não poderá ser registrada.
<h4> Tecnologias Utilizadas no projeto:  💻  </h4>
- API Web do ASP.Net Core 5; <br>
- MongoDb;
<h4>Modelos utilizados:  🏛  </h4>
- MVC;<br>
- Domain Model Pattern;
