using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloProduto;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompra;

public class ListaCompra : EntidadeBase
{
    public String Nome { get; set; }
    public List<ItemListaCompra> Itens { get; set; } = new();
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
        return Itens.Sum(item => item.Quantidade);
    }

     public decimal CalcularTotal()
    {
        decimal total = 0;

        foreach (var item in Itens)
            total += item.CalcularTotal();

        return total;
    }

    public bool PossuiItens()
    {
        return Itens.Count > 0;
    }

    public int ObterQuantidadeItens()
    {
    return Itens.Count;
    }
    
    public bool ProdutoJaExiste(Produto produto)
    {
        return Itens.Any(i => i.Produto.Id == produto.Id);
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
