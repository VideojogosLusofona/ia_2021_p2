<!--
Projeto 2 (alternativo) de Inteligência Artificial 2021/22 (c) by Nuno Fachada

Projeto 2 (alternativo) de Inteligência Artificial 2021/22 is licensed under a
Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.

You should have received a copy of the license along with this
work. If not, see <http://creativecommons.org/licenses/by-nc-sa/4.0/>.
-->

# Projeto 2 (alternativo) de Inteligência Artificial 2021/22

## Introdução

Os grupos devem implementar, em Unity 2021.1, um clone do jogo clássico [Lunar
Lander] baseado no projeto inicial incluído neste repositório.

O projetos têm de ser desenvolvidos por **grupos de 2 a 3 alunos** (não são
permitidos grupos individuais). Até **3 de junho** é necessário que:

* Indiquem a composição do grupo.
* Clonem este repositório para um repositório **privado** no Github.
* Indiquem o URL do repositório **privado** do vosso projeto no GitHub.
* Convidem o [docente][Nuno Fachada] para ter acesso a esse repositório privado.

Os projetos serão automaticamente clonados às **23h55 de 12 de junho** sendo a
entrega feita desta forma, sem intervenção dos alunos. Repositórios e/ou
projetos não funcionais nesta data não serão avaliados.

## O projeto incluído neste repositório

O projeto incluído neste repositório consiste numa versão muito preliminar do
[Lunar Lander] no qual é possível controlar o módulo lunar usando as teclas
&#8592; (rodar para a esquerda), &#8593; (ligar propulsores), &#8594; (rodar
para a direita). Não está implementado qualquer cenário nem colisões.

Embora seja possível mudar algumas constantes (gravidade, grau de rotação,
força aplicada durante a propulsão), é necessário manter os seguintes
princípios inalterados:

* O módulo lunar está sujeito a física (isto é, a movimento dinâmico).
* A propulsão implica a aplicação de uma força ao módulo lunar no sentido para
  o qual ele está virado.
  * É possível limitar a velocidade máxima do módulo se isso for conveniente.
* A rotação é aplicada diretamente, não estando sujeita a forças (isto é,
  movimento cinemático).

Para terem uma ideia da versão original do jogo, podem jogar a versão muito
semelhante [neste link](https://arcader.com/lunar-lander/).

## Requisitos do projeto

### Implementação básica

* O _lander_ deve ter uma quantidade limitada de combustível, que diminui quando
  os propulsores estão ligados.
* Quando o combustível atinge zero, os propulsores deixam de funcionar e o
  módulo lunar fica sujeito apenas à força da gravidade (embora a rotação ainda
  funcione).
* Cada cenário deve ser gerado procedimentalmente (ver próxima secção) e ter
  apenas um local de aterragem horizontal com o dobro da dimensão horizontal do
  _lander_.
* O UI deve indicar a quantidade de combustível disponível, as velocidades
  horizontais e verticais do módulo em cada momento, bem como a distância até ao
  centro do local de aterragem.
* Cada sessão/cenário tem apenas dois desfechos: sucesso ou insucesso. Uma
  aterragem de sucesso exige que o módulo aterre na zona de aterragem com
  rotação zero e velocidade geral (`rb.velocity.magnitude`) abaixo de um valor
  bastante pequeno (a definir pelos alunos). Caso contrário o _lander_ é
  destruído e o jogo termina em insucesso.

### Geração procedimental de conteúdos

* Os cenários de aterragem devem ser gerados procedimentalmente, usando por
  exemplo o algoritmo de _midpoint displacement_ dado nas aulas (código no
  Moodle). A zona de aterragem horizontal deve ser colocada aleatoriamente numa
  parte do cenário e deve ser claramente marcada com uma cor diferente.
* O céu estrelado deve ser gerado usando um _quasi-random number generator_,
  como por exemplo o gerador de Halton dado nas aulas (código no Moodle).
* Podem usar outras alternativas tanto para o cenários de aterragem, como para o
  céu, desde que bem justificadas e referenciadas.

### _Polish_

Antes de passarem para a parte de aprendizagem, devem polir muito bem o vosso
protótipo, sendo as aterragens desafiantes mas não muito difíceis para o jogador
humano. Após chegarem a um conjunto interessante de parâmetros, não os
modifiquem mais. Caso contrário poderão ter de iterar várias vezes a fase de
aprendizagem, complicando e atrasando a finalização deste projeto.

### Aprendizagem com Naive Bayes Classifier

Esta é a parte principal do projeto. Em primeiro lugar, convém definir
claramente os _inputs_ e _outputs_ do(s) classificador(es) de Bayes. Fica uma
sugestão:

* _**Inputs**_ (estado do jogo):
  * Quantidade de combustível
  * Velocidade horizontal
  * Velocidade vertical
  * Rotação
  * Distância horizontal ao centro da zona de aterragem
  * Distância vertical em linha reta ao chão (seja zona de aterragem ou não)
* _**Outputs**_ (ação a aplicar dado o estado do jogo):
  * Rotação para a direita (RD)
  * Rotação para a esquerda (RE)
  * Propulsores ligados (P)

Os _outputs_ podem ocorrer em simultâneo, sendo também possível não ocorrer
nenhum. É possível (e poderá ser extremamente útil) considerar os três _outputs_
como um só, tal como acontece no código incluído neste repositório.

O(s) classificador(es) de Bayes deve(m) ser treinado(s) com observações
capturadas durante os jogos do jogador humano, sendo obtidas em intervalos de
tempo pré-definidos (curtos e constantes, por exemplo a cada 0.2 segundos). Por
exemplo, num jogo com a duração de um minuto podem ser capturadas várias
centenas de observações. Eis alguns exemplos de observações capturadas durante
um jogo:

| Combustível | Vel. horiz. | Vel. vert. | Rot. | Dist. Horiz. LZ | Dist. chão | _Output_  |
|------------:|------------:|-----------:|-----:|----------------:|-----------:|-----------|
| 32.04       | 3.71        |     -13.11 | 0.5  |            92.1 | 26.33      | RD,T      |
| 10.2        | -0.05       |      -0.17 | 0.0  |             0.2 | 0.5        | --        |
| 19.3        | -18.14      |     -33.10 | 30.4 |            50.1 | 29.54      | RE,T      |

Tal como no exemplo dado em aula, devem discretizar os valores dos _inputs_ da
vossa implementação de modo a que os possam introduzir no classificador de
Bayes. Por exemplo (e é apenas um exemplo, poderão ter de experimentar
diferentes discretizações), o nível de combustível poderá ser mapeado em quatro
valores discretos: atestado, meio depósito, na reserva e vazio.

### Menu principal e fluxo do programa

Tal como no exemplo Bayes Monsters, o menu deve ter três opções:

* _**Human Game**_: _lander_ controlado pelo jogador humano
* _**AI Game**_: _lander_ controlado pelo classificador de Bayes, que aprendeu com os
  exemplos humanos.
* _**Quit**_: Sair da aplicação

No primeiro caso, _**Human Game**_, o jogador humano controla o _lander_. As
decisões do jogador humano vão sendo capturadas para possível inclusão no
classificador de Bayes. No fim do jogo, quer o jogador tenha tido sucesso ou
não, deve ser apresentada uma caixa de diálogo indicando quantas observações
foram capturadas, e perguntando ao jogador se as quer passar ao classificador de
Bayes para fins de aprendizagem. De modo a que a AI aprenda mais rapidamente,
o jogador deve responder sim apenas se tiver finalizado o jogo com sucesso ou
muito perto disso. Após a pergunta ter sido respondida, o programa volta ao
menu principal.

No segundo caso, _**AI Game**_, o classificador de Bayes controlará o _lander_.
Espera-se que, após alguns jogos de sucesso do jogador humano, o classificador
de Bayes seja também capaz de controlar o _lander_ com sucesso.

A terceira opção, _**Quit**_, é auto-explicativa.

Em qualquer dos casos, poderá ser interessante ter o número de observações
(efetivamente passadas ao classificador de Bayes) indicado permanentemente no
UI.

### Notas adicionais

Para implementação deste projeto é crucial entender bem as partes relevantes de
PCG (_midpoint displacement_ e gerador de Halton), bem como classificadores de
Bayes. Além de um bom entendimento teórico, será certamente importante perceber
o código de dois dos exemplos dados em aula (disponíveis no Moodle):

* Projeto Procedural2D
* Projeto Bayes Monsters

Se tiverem dúvidas sobre como proceder em alguma fase do projeto, entrem em
contacto (pelo menos uma semana antes do prazo de entrega).

## Código e Git

O código do projeto, bem como o uso de Git, devem seguir as melhores práticas
lecionadas em LP1 e LP2 e descritas nos diferentes enunciados dessas
disciplinas.

Estas componentes não são o foco principal desta avaliação, mas projetos mal
programados e com fraco uso de Git serão penalizados.

Todos os membros do grupo devem contribuir para o repositório, de preferência
tanto para o código como para o relatório.

## Relatório

_Em breve_

## Entrega

O projeto é entregue de forma automática através do GitHub. Mais concretamente,
o repositório do projeto será automaticamente clonado às **23h55 de 12 de junho
de 2022**. Certifiquem-se de que a aplicação está funcional e que todos os
requisitos foram cumpridos, caso contrário o projeto não será avaliado.

O repositório deve ter:

* Projeto Unity funcional segundo os requisitos indicados.
* Ficheiros `.gitignore` e `.gitattributes` adequados para projetos Unity
  (já estão incluídos neste repositório).
* Ficheiro `README.md` contendo o relatório do projeto em formato [Markdown]
  (podem editar diretamente este ficheiro após fazerem o _clone_ deste
  repositório).
* Ficheiros de imagens, contendo os diagramas figuras que considerem úteis.
  Estes ficheiros devem ser incluídos no repositório em modo Git LFS (assim como
  todos os _assets_ binários do Unity).

Em nenhuma circunstância o repositório pode ter _builds_ ou outros ficheiros
temporários do Unity (que são automaticamente ignorados se usarem um
`.gitignore` apropriado).

## Honestidade académica

Nesta disciplina, espera-se que cada aluno siga os mais altos padrões de
honestidade académica. Isto significa que cada ideia que não seja do
aluno deve ser claramente indicada, com devida referência ao respetivo
autor. O não cumprimento desta regra constitui plágio.

O plágio inclui a utilização de ideias, código ou conjuntos de soluções
de outros alunos ou indivíduos, ou quaisquer outras fontes para além
dos textos de apoio à disciplina, sem dar o respetivo crédito a essas
fontes. Os alunos são encorajados a discutir os problemas com outros
alunos e devem mencionar essa discussão quando submetem os projetos.
Essa menção **não** influenciará a nota. Os alunos não deverão, no
entanto, copiar códigos, documentação e relatórios de outros alunos, ou dar os
seus próprios códigos, documentação e relatórios a outros em qualquer
circunstância. De facto, não devem sequer deixar códigos, documentação e
relatórios em computadores de uso partilhado.

Nesta disciplina, a desonestidade académica é considerada fraude, com
todas as consequências legais que daí advêm. Qualquer fraude terá como
consequência imediata a anulação dos projetos de todos os alunos envolvidos
(incluindo os que possibilitaram a ocorrência). Qualquer suspeita de
desonestidade académica será relatada aos órgãos superiores da escola
para possível instauração de um processo disciplinar. Este poderá
resultar em reprovação à disciplina, reprovação de ano ou mesmo suspensão
temporária ou definitiva da ULHT.

*Texto adaptado da disciplina de [Algoritmos e
Estruturas de Dados][aed] do [Instituto Superior Técnico][ist]*

## Referências

* Millington, I. (2019). AI for Games (3rd ed.). CRC Press.

## Licenças

* Este enunciado é disponibilizado através da licença [CC BY-NC-SA 4.0].
* O código que acompanha este enunciado é disponibilizado através da licença
  [MPL 2.0].

## Metadados

* Autor: [Nuno Fachada]
* Curso:  [Licenciatura em Videojogos][lamv]
* Instituição: [Universidade Lusófona de Humanidades e Tecnologias][ULHT]

[CC BY-NC-SA 4.0]:https://creativecommons.org/licenses/by-nc-sa/4.0/
[MPL 2.0]:https://www.mozilla.org/en-US/MPL/2.0/
[lamv]:https://www.ulusofona.pt/licenciatura/videojogos
[Nuno Fachada]:https://github.com/fakenmc
[ULHT]:https://www.ulusofona.pt/
[aed]:https://fenix.tecnico.ulisboa.pt/disciplinas/AED-2/2009-2010/2-semestre/honestidade-academica
[ist]:https://tecnico.ulisboa.pt/pt/
[Markdown]:https://guides.github.com/features/mastering-markdown/
[SOLID]:https://en.wikipedia.org/wiki/SOLID
[KISS]:https://en.wikipedia.org/wiki/KISS_principle
[XML]:https://docs.microsoft.com/dotnet/csharp/codedoc
[Lunar Lander]:https://en.wikipedia.org/wiki/Lunar_Lander_(1979_video_game)
