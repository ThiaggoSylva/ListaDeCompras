using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.ModuloProduto;

public class Produto : EntidadeBase
{
    public string Nome { get; set; }
    public Categoria Categoria { get; set; }
    public string UnidadeMedida { get; set; }
    public decimal PrecoAproximado { get; set; }

    public Produto(string nome, Categoria categoria, string unidadeMedida, decimal precoAproximado)
    {
        Nome = nome;
        Categoria = categoria;
        UnidadeMedida = unidadeMedida;
        PrecoAproximado = precoAproximado;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Produto produtoAtualizado = (Produto)entidadeAtualizada;

        Nome = produtoAtualizado.Nome;
        Categoria = produtoAtualizado.Categoria;
        UnidadeMedida = produtoAtualizado.UnidadeMedida;
        PrecoAproximado = produtoAtualizado.PrecoAproximado;
    }

    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O nome deve conter entre 2 e 100 caracteres.");

        if (Categoria == null)
            erros.Add("A categoria é obrigatória.");

        if (string.IsNullOrWhiteSpace(UnidadeMedida))
            erros.Add("A unidade de medida é obrigatória.");

        if (PrecoAproximado < 0)
            erros.Add("O preço aproximado não pode ser negativo.");

        return erros.ToArray();
    }
}