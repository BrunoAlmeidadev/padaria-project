# Sistema para a Padaria "Pão Precioso" 🥖

## Sobre o Projeto 📋

A padaria "Pão Precioso", comandada pelo Seu Antônio, até hoje realiza tudo na base do caderno: vendas, controle de estoque, validade dos produtos... tudo na base do olho e do papel. Isso não só dá trabalho, mas também abre espaço para erros, desperdícios e problemas sanitários.

O objetivo deste sistema é digitalizar e organizar todos os processos da padaria, ajudando o Seu Antônio a economizar tempo, evitar prejuízos e até melhorar a qualidade do atendimento. 🚀

Ah, e para dar um toque especial, incluí algumas melhorias no sistema para facilitar ainda mais a vida do Seu Antônio!

---

## Funcionalidades 💡

- **Controle de Estoque**: Registro dos produtos, suas quantidades e datas de validade. Nada de ficar conferindo visualmente o que acabou!
- **Vendas Automatizadas**: Registre as vendas e formas de pagamento direto no sistema.
- **Fechamento de Caixa**: Soma automática de tudo que foi vendido no dia. Adeus, calculadora e somas manuais.
- **Relatórios**:
  - ProjetoPadaria/bin/Debug/net8.0
  - Relatorio Vendas (em `.txt`);
  - Inventario (em `.txt`);
- **Busca de Produtos**: Aplicação de busca binária para encontrar produtos rapidamente.
- **Organização por Ordem Alfabética**: Produtos sempre ordenados para facilitar o gerenciamento.
- **Gravação de Dados**:
  - Produtos salvos em arquivo `.bin` para garantir persistência.

E, como diferencial, pensei em algo a mais para ajudar o Seu Antônio. Que tal um **aviso automático para produtos próximos da validade** e uma **sugestão de reposição de estoque** com base nas vendas anteriores? 📈

---

## Tecnologias Usadas 🛠️

- **Linguagem**: C# (com a versão mais recente do `.NET 8`);
- **Estruturas de Dados**: Uso de `structs` para organização;
- **Métodos e Procedimentos**: Tudo bem modularizado;
- **Recursividade**: Sim, tem uma pitada disso aqui também;
- **Comandos**: `if`, `for`, `while`, `switch`... tá tudo aqui!

---

## Como Rodar o Projeto ▶️

1. **Pré-requisitos**:  
   Certifique-se de ter o `.NET 6 ou superior` instalado na sua máquina.

2. **Clone o Repositório**:
   ```bash
   git clone https://github.com/BrunoAlmeidadev/padaria-project.git
   cd ProjetoPadaria
Compile e Execute:

bash
Copiar código
dotnet run
Siga os Menus:
O sistema é bem intuitivo, com menus interativos para cada funcionalidade. Só seguir as opções e pronto!

Estrutura do Projeto 🗂️
Padaria-projeto.cs: Arquivo principal que inicializa o sistema.
Estoque.bin: Arquivo binário onde ficam salvos os dados dos produtos.
RelatorioFluxoCaixa.txt: Relatório diário do fluxo de caixa.
Inventario.txt: Relatório com os produtos do estoque.
Diferenciais do Projeto 🌟
Além das funcionalidades básicas pedidas, implementei algumas melhorias para facilitar a vida do Seu Antônio:

Aviso de Produtos Perto da Validade: O sistema alerta automaticamente os produtos que precisam ser vendidos com urgência.
Sugestão de Reposição: Com base nas vendas registradas, o sistema sugere o que precisa ser comprado, sem precisar perder tempo conferindo prateleiras.
Considerações Finais ✨
Esse projeto foi desenvolvido pensando no dia a dia da padaria "Pão Precioso", mas pode ser facilmente adaptado para outros pequenos negócios.

Seu Antônio agora pode focar no que realmente importa: fazer aquele pãozinho delicioso enquanto o sistema cuida do resto! 🥖💻
