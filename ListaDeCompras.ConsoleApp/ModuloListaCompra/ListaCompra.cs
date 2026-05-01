using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class ListaCompra : EntidadeBase
{
    public String Nome { get; set; }
    public DateTime DataCriacao { get; private set; }
    public StatusListaCompra Status { get; set; }
    public List<Produto> Produtos { get; private set; }

    public ListaCompra(string nome, StatusListaCompra status)
    {
        Nome = nome;
        DataCriacao = DateTime.Now;
        Status = status;
        Produtos = new List<Produto>();
    }

    public int CalcularTotalItens()
    {
        return Produtos.Count;
    }

    public decimal CalcularTotalEstimado()
    {
        decimal total = 0;
        foreach (Produto produto in Produtos)
            total += produto.PrecoAproximado;

        return total;
    }
    
    public bool PossuiItensVinculados()
    {
        return Produtos.Count > 0;
    }

    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O nome da Lista é obrigatório e deve conter entre 3 e 100 caracteres.");

        if (!Enum.IsDefined(typeof(StatusListaCompra), Status))
            erros.Add("Status da Lista de Compra é Obrigatório.");
            
        return erros.ToArray();
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        ListaCompra listaAtualizada = (ListaCompra)entidadeAtualizada;
        
        Nome = listaAtualizada.Nome;
        Status = listaAtualizada.Status;
    }
}
