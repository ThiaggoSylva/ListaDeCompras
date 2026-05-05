using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloProduto;
using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompras;

public class TelaListaCompras : TelaBase<ListaCompras>, ITelaOpcoes, ITelaCrud
{
    private readonly RepositorioProdutoEmArquivo repositorioProduto;

    public TelaListaCompras(
        RepositorioListaComprasEmArquivo repositorioListaCompras,
        RepositorioProdutoEmArquivo repositorioProduto
    ) : base("Lista de Compras", repositorioListaCompras)
    {
        this.repositorioProduto = repositorioProduto;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Lista de Compras");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar lista de compras");
        Console.WriteLine($"2 - Editar  lista de compras");
        Console.WriteLine($"3 - Excluir lista de compras");
        Console.WriteLine($"4 - Visualizar listas de compras");
        Console.WriteLine($"5 - Adicionar item à lista de compras");
        Console.WriteLine($"6 - Remover item da lista de compras");
        Console.WriteLine($"7 - Visualizar itens de listas de compras");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void AdicionarItem()
    {
        ExibirCabecalho("Adição de Item de Listas de Compras");

        VisualizarTodos(false);

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID da lista que deseja gerenciar (ou S para sair): ");
        string idSelecionado = Console.ReadLine() ?? string.Empty;

        if (idSelecionado.ToUpper() == "S")
            return;

        ListaCompras? listaSelecionada = repositorio.SelecionarPorId(idSelecionado);

        if (listaSelecionada == null)
        {
            Notificador.ExibirMensagem("Não foi possível encontrar a lista de compras selecionada.");
            return;
        }

        VisualizarItens(listaSelecionada);

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Selecione um produto abaixo");
        Console.WriteLine("---------------------------------");

        VisualizarProdutos();

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do produto que deseja adicionar (ou S para sair): ");
        string idProdutoSelecionado = Console.ReadLine() ?? string.Empty;

        if (idProdutoSelecionado.ToUpper() == "S")
            return;

        Produto? produtoSelecionado = repositorioProduto.SelecionarPorId(idProdutoSelecionado);

        if (produtoSelecionado == null)
        {
            Notificador.ExibirMensagem("Não foi possível encontrar o produto selecionado.");
            return;
        }

        Console.Write("Digite a quantidade do produto que deseja adicionar: ");
        int quantidadeItens = Convert.ToInt32(Console.ReadLine());

        listaSelecionada.AdicionarItem(produtoSelecionado, quantidadeItens);

        Notificador.ExibirMensagem($"O item \"{produtoSelecionado.Nome}\" foi adicionado à lista com sucesso!");
    }

    public void RemoverItem()
    {
        ExibirCabecalho("Remoção de Item de Listas de Compras");

        VisualizarTodos(false);

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID da lista que deseja gerenciar (ou S para sair): ");
        string idSelecionado = Console.ReadLine() ?? string.Empty;

        if (idSelecionado.ToUpper() == "S")
            return;

        ListaCompras? listaSelecionada = repositorio.SelecionarPorId(idSelecionado);

        if (listaSelecionada == null)
        {
            Notificador.ExibirMensagem("Não foi possível encontrar a lista de compras selecionada.");
            return;
        }

        VisualizarItens(listaSelecionada);

        Console.WriteLine("---------------------------------");

        Console.Write("Digite o ID do item da lista que deseja remover (ou S para sair): ");
        string idItemSelecionado = Console.ReadLine() ?? string.Empty;

        if (idItemSelecionado.ToUpper() == "S")
            return;

        bool conseguiuRemover = listaSelecionada.RemoverItem(idItemSelecionado);

        if (!conseguiuRemover)
        {
            Notificador.ExibirMensagem("Não é possível encontrar o item da lista.");
            return;
        }

        Notificador.ExibirMensagem($"O item foi removido da lista com sucesso!");
    }

    public void VisualizarItens(ListaCompras? listaSelecionada = null)
    {
        if (listaSelecionada == null)
        {
            ExibirCabecalho("Visualização de Item de Listas de Compras");

            VisualizarTodos(false);

            Console.WriteLine("---------------------------------");

            Console.Write("Digite o ID da lista que deseja gerenciar (ou S para sair): ");
            string idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.ToUpper() == "S")
                return;

            listaSelecionada = repositorio.SelecionarPorId(idSelecionado);

            if (listaSelecionada == null)
            {
                Notificador.ExibirMensagem("Não foi possível encontrar a lista de compras selecionada.");
                return;
            }
        }

        List<ItemListaCompras> itens = listaSelecionada.Itens;

        if (itens.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum item registrado.");
            return;
        }
        else
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Itens atuais da lista \"{listaSelecionada.Nome}\"");
            Console.WriteLine("---------------------------------");

            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -15}",
                "Id", "Nome do Produto", "Quantidade", "Preço (R$)"
            );

            Console.ForegroundColor = ConsoleColor.Yellow;

            foreach (ItemListaCompras i in itens)
            {
                Console.WriteLine(
                    "{0, -7} | {1, -30} | {2, -15} | {3, -15}",
                    i.Id, i.Produto.Nome, i.Quantidade, i.Preco.ToString("C2")
                );
            }

            Console.ResetColor();
        }
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Listas de Compras");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
            "Id", "Nome", "Criação", "Qtd. Itens", "Total Gasto (R$)"
        );

        List<ListaCompras> listas = repositorio.SelecionarTodos();

        foreach (ListaCompras l in listas)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
                l.Id, l.Nome, l.DataCriacao.ToShortDateString(), l.Itens.Count, l.TotalGasto.ToString("C2")
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override ListaCompras ObterDadosCadastrais()
    {
        Console.Write("Digite o nome da lista: ");
        string nome = Console.ReadLine() ?? string.Empty;

        return new ListaCompras(nome);
    }

    protected override List<string> ValidarExclusaoRegistro(ListaCompras registro)
    {
        List<string> erros = new List<string>();

        if (registro.Itens.Count > 0)
            erros.Add("Não é possível excluir uma lista de compras com itens cadastrados.");

        return erros;
    }

    private void VisualizarProdutos()
    {
        List<Produto> produtos = repositorioProduto.SelecionarTodos();

        if (produtos.Count == 0)
        {
            Notificador.ExibirMensagem("Nenhum produto registrado.");
            return;
        }

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -15}",
            "Id", "Nome", "Medida", "Preço Aproximado", "Categoria"
        );

        foreach (Produto p in produtos)
        {
            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -15}",
                p.Id, p.Nome, p.UnidadeMedida, p.PrecoAproximado.ToString("C2"), p.Categoria.Nome
            );
        }
    }
}
