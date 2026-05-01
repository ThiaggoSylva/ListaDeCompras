using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloProduto;
using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class TelaListaCompra : TelaBase<ListaCompra>
{
    private readonly RepositorioListaCompra repositorioListaCompra;
    private TelaProduto telaProduto;

   public TelaListaCompra(
    RepositorioListaCompra repositorioListaCompra,
    TelaProduto telaProduto
    ) : base("Lista de Compras", repositorioListaCompra)
    {
    this.repositorioListaCompra = repositorioListaCompra;
    this.telaProduto = telaProduto;
    }
    protected override ListaCompra ObterDadosCadastrais()
    {
        Console.Write("Digite o nome da lista de compras: ");
        string nome = Console.ReadLine() ?? string.Empty;

        StatusListaCompra status = ObterStatus();

        return new ListaCompra(nome, status);
    }

        public void GerenciarItens()
{
    ListaCompra? lista = SelecionarLista();

    if (lista == null)
        return;

    while (true)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Itens da Lista: {lista.Nome}");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Adicionar item");
        Console.WriteLine("2 - Remover item");
        Console.WriteLine("3 - Visualizar itens");
        Console.WriteLine("S - Voltar");
        Console.Write("> ");

        string? opcao = Console.ReadLine()?.ToUpper();

        switch (opcao)
        {
            case "1":
                AdicionarItem(lista);
                break;

            case "2":
                RemoverItem(lista);
                break;

            case "3":
                VisualizarItens(lista);
                break;

            case "S":
                return;
        }
    }
}

    private StatusListaCompra ObterStatus()
    {
        Console.WriteLine("------------------------");
        Console.WriteLine("Selecione o status da lista:");
        Console.WriteLine("1. Aberta");
        Console.WriteLine("2. Concluída");
        Console.WriteLine("------------------------");
        Console.Write("Escolha uma opção: ");

        string opcao = Console.ReadLine() ?? string.Empty;

        if (opcao == "2")
            return StatusListaCompra.Concluida;

        return StatusListaCompra.Aberta;

    }
    public override void VisualizarTodos(bool exibirCabecalho)
    {
        if (exibirCabecalho)
            ExibirCabecalho("Visualização de Listas de Compras");

        List<ListaCompra> listas = repositorioListaCompra.SelecionarTodos();

        if (listas.Count == 0)
        {
            Console.WriteLine("Nenhuma lista de compras cadastrada.");
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine(
            "{0, -8} | {1, -25} | {2, -15} | {3, -12} | {4, -12} | {5, -15}",
            "ID",
            "Nome",
            "Data Criação",
            "Status",
            "Qtd. Itens",
            "Total Estimado"
        );

       foreach (ListaCompra lista in listas)
        {
            int quantidadeTotalItens = lista.Itens.Sum(item => item.Quantidade);

    Console.WriteLine(
        "{0, -8} | {1, -25} | {2, -15} | {3, -12} | {4, -12} | R$ {5, -12:F2}",
        lista.Id,
        lista.Nome,
        lista.DataCriacao.ToShortDateString(),
        lista.Status,
        quantidadeTotalItens,
        lista.CalcularTotal()
        );
        }

        Console.WriteLine("---------------------------------");

        if (exibirCabecalho)
        {
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public override void Excluir()
    {
        ExibirCabecalho("Exclusão de Lista de Compras");

        VisualizarTodos(false);

        Console.Write("Digite o ID da lista que deseja excluir: ");
        string idSelecionado = Console.ReadLine() ?? string.Empty;

        ListaCompra? listaSelecionada = repositorioListaCompra.SelecionarPorId(idSelecionado);

        if (listaSelecionada == null)
        {
            Notificador.ExibirMensagem("ID inválido. Nenhuma lista encontrada com o ID informado.");
            return;
        }

        if (listaSelecionada.PossuiItens())
        {
            Notificador.ExibirMensagem("Não é possível excluir uma lista que possui itens vinculados.");
            return;
        }

        repositorioListaCompra.Excluir(idSelecionado);

        Notificador.ExibirMensagem("Lista de compras excluída com sucesso.");

    }

    private void AdicionarItem(ListaCompra lista)
    {
        ExibirCabecalho("Adicionar Item");

        Produto? produto = telaProduto.SelecionarProduto();

        if (produto == null)
            return;

        bool produtoJaExiste = lista.Itens.Any(item =>
            item.Produto.Id == produto.Id
        );

        if (produtoJaExiste)
        {
        Notificador.ExibirMensagem("Este produto já existe na lista.");
        return;
        }

        Console.Write("Quantidade: ");
        int quantidade = Convert.ToInt32(Console.ReadLine());

        if (quantidade <= 0)
        {
            Notificador.ExibirMensagem("Quantidade deve ser maior que zero!");
            return;
        }

        lista.Itens.Add(new ItemListaCompra(produto, quantidade));

        Notificador.ExibirMensagem("Item adicionado com sucesso!");
    }

        private void RemoverItem(ListaCompra lista)
        {
        ExibirCabecalho("Remover Item");

        if (!lista.PossuiItens())
        {
            Notificador.ExibirMensagem("Lista não possui itens!");
            return;
        }

        for (int i = 0; i < lista.Itens.Count; i++)
        {
            var item = lista.Itens[i];
            Console.WriteLine($"{i + 1} - {item.Produto.Nome}");
        }

        Console.Write("Selecione: ");
        int indice = Convert.ToInt32(Console.ReadLine()) - 1;

        if (indice < 0 || indice >= lista.Itens.Count)
            return;

        lista.Itens.RemoveAt(indice);

        Notificador.ExibirMensagem("Item removido!");
        }

        private void VisualizarItens(ListaCompra lista)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Itens da Lista: {lista.Nome}");
        Console.WriteLine("---------------------------------");

        if (lista.Itens.Count == 0)
    {
        Console.WriteLine("Lista vazia!");
    }
        else
    {
        foreach (ItemListaCompra item in lista.Itens)
        {
            Console.WriteLine(
                $"Produto: {item.Produto.Nome} | " +
                $"Categoria: {item.Produto.Categoria.Nome} | " +
                $"Qtd: {item.Quantidade} | " +
                $"Total: R$ {item.CalcularTotal():F2}"
            );
        }

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"TOTAL DA LISTA: R$ {lista.CalcularTotal():F2}");
    }

    Console.WriteLine("---------------------------------");
    Console.Write("Digite ENTER para continuar...");
    Console.ReadLine();
    }

    private ListaCompra? SelecionarLista()
    {
        ExibirCabecalho("Seleção de Listas");

        VisualizarTodos(false);

        Console.Write("Digite o Id da lista: ");
        string id = Console.ReadLine()!;

        ListaCompra? lista = repositorioListaCompra.SelecionarPorId(id);

        if (lista == null)
            Notificador.ExibirMensagem("Lista não encontrada!");

        return lista;
    }


}