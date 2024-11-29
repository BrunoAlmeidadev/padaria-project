using System;
using System.IO;

class Program
{
    struct Produto
    {
        public string descricao;
        public DateTime validade;
        public int quantidade;
        public float preco;
    }

    struct Venda
    {
        public int produtoId;
        public int quantidade;
        public DateTime data;
    }

    static void Main(string[] args)
    {
        Produto[] estoque = new Produto[100];
        Venda[] vendas = new Venda[100];
        int qEstoque = 0, qVendas = 0;

        LerDadosEstoque(estoque, ref qEstoque);
        LerDadosVendas(vendas, ref qVendas);

        int opcao;
        do
        {
            opcao = MenuPrincipal();
            switch (opcao)
            {
                case 1:
                    MenuEstoque(estoque, ref qEstoque);
                    break;
                case 2:
                    RegistrarVenda(estoque, ref qEstoque, vendas, ref qVendas);
                    break;
                case 3:
                    MenuRelatorios(estoque, qEstoque, vendas, qVendas);
                    break;
            }
        } while (opcao != 0);

        GravarDadosEstoque(estoque, qEstoque);
        GravarDadosVendas(vendas, qVendas);
    }

    static int MenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("==== Padaria Pão Precioso ====\n");
        Console.WriteLine("1 - Gerenciar Estoque");
        Console.WriteLine("2 - Registrar Vendas");
        Console.WriteLine("3 - Relatórios");
        Console.WriteLine("0 - Sair\n");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine());
    }

    static void MenuEstoque(Produto[] estoque, ref int qEstoque)
    {
        Console.Clear();
        Console.WriteLine("==== Gerenciar Estoque ====\n");
        Console.WriteLine("1 - Cadastrar Produto");
        Console.WriteLine("2 - Verificar Validade");
        Console.WriteLine("0 - Voltar\n");
        Console.Write("Escolha uma opção: ");
        int opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                CadastrarProduto(estoque, ref qEstoque);
                break;
            case 2:
                VerificarValidade(estoque, qEstoque);
                break;
        }
    }

    static void MenuRelatorios(Produto[] estoque, int qEstoque, Venda[] vendas, int qVendas)
    {
        Console.Clear();
        Console.WriteLine("==== Relatórios ====\n");
        Console.WriteLine("1 - Fluxo de Caixa");
        Console.WriteLine("2 - Inventário");
        Console.WriteLine("0 - Voltar\n");
        Console.Write("Escolha uma opção: ");
        int opcao = int.Parse(Console.ReadLine());

        switch (opcao)
        {
            case 1:
                GerarRelatorioVendas(vendas, qVendas, estoque);
                break;
            case 2:
                GerarInventario(estoque, qEstoque);
                break;
        }
    }

    static void CadastrarProduto(Produto[] estoque, ref int qEstoque)
    {
        if (qEstoque >= estoque.Length)
        {
            Console.WriteLine("Estoque cheio! Não é possível adicionar mais produtos.");
            Console.ReadKey();
            return;
        }

        Produto produto;
        Console.WriteLine("==== Cadastro de Produto ====");
        Console.Write("Descrição: ");
        produto.descricao = Console.ReadLine();
        Console.Write("Validade (dd-MM-yyyy): ");
        produto.validade = DateTime.Parse(Console.ReadLine());
        Console.Write("Quantidade: ");
        produto.quantidade = int.Parse(Console.ReadLine());
        Console.Write("Preço: ");
        produto.preco = float.Parse(Console.ReadLine());
        estoque[qEstoque] = produto;
        qEstoque++;

        Console.WriteLine("Produto cadastrado com sucesso!");
        Console.ReadKey();
    }

    static void VerificarValidade(Produto[] estoque, int qEstoque)
    {
        Console.WriteLine("==== Produtos Fora da Validade ====");
        DateTime hoje = DateTime.Today;
        for (int i = 0; i < qEstoque; i++)
        {
            if (estoque[i].validade < hoje)
            {
                Console.WriteLine($"Produto: {estoque[i].descricao}, Validade: {estoque[i].validade:dd/MM/yyyy}");
            }
        }
        Console.ReadKey();
    }

    static void RegistrarVenda(Produto[] estoque, ref int qEstoque, Venda[] vendas, ref int qVendas)
    {
        if (qVendas >= vendas.Length)
        {
            Console.WriteLine("Limite de vendas atingido! Não é possível registrar mais vendas.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("==== Registrar Venda ====");
        Console.Write("Descrição do Produto: ");
        string descricao = Console.ReadLine();

        // Ordenar o vetor antes da busca binária.
        OrdenarProdutos(estoque, qEstoque);

        int index = BuscaBinaria(estoque, descricao, qEstoque);

        if (index >= 0)
        {
            Produto produto = estoque[index];
            Console.Write("Quantidade: ");
            int quantidade = int.Parse(Console.ReadLine());

            if (produto.quantidade >= quantidade)
            {
                produto.quantidade -= quantidade;
                estoque[index] = produto;

                Venda venda = new Venda
                {
                    produtoId = index,
                    quantidade = quantidade,
                    data = DateTime.Now
                };
                vendas[qVendas] = venda;
                qVendas++;

                Console.WriteLine($"Venda registrada! Total: R$ {produto.preco * quantidade:F2}");
            }
            else
            {
                Console.WriteLine("Quantidade insuficiente no estoque.");
            }
        }
        else
        {
            Console.WriteLine("Produto não encontrado.");
        }
        Console.ReadKey();
    }

    static void GerarRelatorioVendas(Venda[] vendas, int qVendas, Produto[] estoque)
    {
        using (StreamWriter writer = new StreamWriter("RelatorioVendas.txt"))
        {
            Console.WriteLine("==== Relatório de Vendas ====");
            writer.WriteLine("==== Relatório de Vendas ====");
            float total = 0;
            for (int i = 0; i < qVendas; i++)
            {
                Produto produto = estoque[vendas[i].produtoId];
                float subtotal = produto.preco * vendas[i].quantidade;
                total += subtotal;
                Console.WriteLine($"Produto: {produto.descricao}, Quantidade: {vendas[i].quantidade}, Subtotal: R$ {subtotal:F2}");
                writer.WriteLine($"Produto: {produto.descricao}, Quantidade: {vendas[i].quantidade}, Subtotal: R$ {subtotal:F2}");
            }
            Console.WriteLine($"Total: R$ {total:F2}");
            writer.WriteLine($"Total: R$ {total:F2}");
        }
        Console.ReadKey();
    }

    static void GerarInventario(Produto[] estoque, int qEstoque)
    {
        using (StreamWriter writer = new StreamWriter("Inventario.txt"))
        {
            Console.WriteLine("==== Inventário ====");
            writer.WriteLine("==== Inventário ====");
            OrdenarProdutos(estoque, qEstoque);
            for (int i = 0; i < qEstoque; i++)
            {
                Console.WriteLine($"Produto: {estoque[i].descricao}, Quantidade: {estoque[i].quantidade}, Preço: R$ {estoque[i].preco:F2}");
                writer.WriteLine($"Produto: {estoque[i].descricao}, Quantidade: {estoque[i].quantidade}, Preço: R$ {estoque[i].preco:F2}");
            }
        }
        Console.ReadKey();
    }

    static void GravarDadosEstoque(Produto[] estoque, int qEstoque)
    {
        using (FileStream fs = new FileStream("estoque.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(qEstoque);
            for (int i = 0; i < qEstoque; i++)
            {
                writer.Write(estoque[i].descricao);
                writer.Write(estoque[i].validade.ToBinary());
                writer.Write(estoque[i].quantidade);
                writer.Write(estoque[i].preco);
            }
        }
    }

    static void LerDadosEstoque(Produto[] estoque, ref int qEstoque)
    {
        if (File.Exists("estoque.bin"))
        {
            using (FileStream fs = new FileStream("estoque.bin", FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                qEstoque = reader.ReadInt32();
                for (int i = 0; i < qEstoque; i++)
                {
                    estoque[i].descricao = reader.ReadString();
                    estoque[i].validade = DateTime.FromBinary(reader.ReadInt64());
                    estoque[i].quantidade = reader.ReadInt32();
                    estoque[i].preco = reader.ReadSingle();
                }
            }
        }
    }

    static void GravarDadosVendas(Venda[] vendas, int qVendas)
    {
        using (FileStream fs = new FileStream("vendas.bin", FileMode.Create))
        using (BinaryWriter writer = new BinaryWriter(fs))
        {
            writer.Write(qVendas);
            for (int i = 0; i < qVendas; i++)
            {
                writer.Write(vendas[i].produtoId);
                writer.Write(vendas[i].quantidade);
                writer.Write(vendas[i].data.ToBinary());
            }
        }
    }

    static void LerDadosVendas(Venda[] vendas, ref int qVendas)
    {
        if (File.Exists("vendas.bin"))
        {
            using (FileStream fs = new FileStream("vendas.bin", FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                qVendas = reader.ReadInt32();
                for (int i = 0; i < qVendas; i++)
                {
                    vendas[i].produtoId = reader.ReadInt32();
                    vendas[i].quantidade = reader.ReadInt32();
                    vendas[i].data = DateTime.FromBinary(reader.ReadInt64());
                }
            }
        }
    }

    static void OrdenarProdutos(Produto[] estoque, int qEstoque)
    {
        for (int i = 0; i < qEstoque - 1; i++)
        {
            for (int j = 0; j < qEstoque - i - 1; j++)
            {
                if (string.Compare(estoque[j].descricao, estoque[j + 1].descricao, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    Produto temp = estoque[j];
                    estoque[j] = estoque[j + 1];
                    estoque[j + 1] = temp;
                }
            }
        }
    }

    static int BuscaBinaria(Produto[] estoque, string descricao, int qEstoque)
    {
        int inicio = 0, fim = qEstoque - 1;
        while (inicio <= fim)
        {
            int meio = (inicio + fim) / 2;
            int comparacao = string.Compare(estoque[meio].descricao, descricao, StringComparison.OrdinalIgnoreCase);
            if (comparacao == 0)
                return meio;
            if (comparacao < 0)
                inicio = meio + 1;
            else
                fim = meio - 1;
        }
        return -1;
    }
}