using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Utilidades;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class TelaListaCompra : TelaBase<ListaCompra>
{
    private readonly RepositorioListaCompra repositorioListaCompra;

    public TelaListaCompra(RepositorioListaCompra repositorioListaCompra) : base(" Lista de Compras", repositorioListaCompra)
    {
        this.repositorioListaCompra = repositorioListaCompra;
    }
    protected override ListaCompra ObterDadosCadastrais()
    {
        Console.Write("Digite o nome da lista de compras: ");
        string nome = Console.ReadLine() ?? string.Empty;

        StatusListaCompra status = ObterStatus();

        return new ListaCompra(nome, status);
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
            Console.WriteLine(
                "{0, -8} | {1, -25} | {2, -15} | {3, -12} | {4, -12} | R$ {5, -12:F2}",
                lista.Id,
                lista.Nome,
                lista.DataCriacao.ToShortDateString(),
                lista.Status,
                lista.CalcularTotalItens(),
                lista.CalcularTotalEstimado()
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

        if (listaSelecionada.PossuiItensVinculados())
        {
            Notificador.ExibirMensagem("Não é possível excluir uma lista que possui itens vinculados.");
            return;
        }

        repositorioListaCompra.Excluir(idSelecionado);

        Notificador.ExibirMensagem("Lista de compras excluída com sucesso.");

    }
}