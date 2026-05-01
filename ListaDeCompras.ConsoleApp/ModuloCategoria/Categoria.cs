namespace ListaDeCompras.ConsoleApp.ModuloCategoria;

using ListaDeCompras.ConsoleApp.Compartilhado;

public class Categoria : EntidadeBase
{
    public string Nome { get; set; }
    public CorCategoria Cor { get; set; }

    public Categoria(string nome, CorCategoria cor)
    {
        Nome = nome;
        Cor = cor;
    }

    public override void AtualizarDados(EntidadeBase entidadeAtualizada)
    {
        Categoria categoriaAtualizada = (Categoria)entidadeAtualizada;

        Nome = categoriaAtualizada.Nome;
        Cor = categoriaAtualizada.Cor;
    }

    public override string[] Validar()
    {
        List<string> erros = new List<string>();

        if (string.IsNullOrWhiteSpace(Nome))
            erros.Add("O nome da categoria é obrigatório.");

        else if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O nome da categoria deve conter entre 2 e 100 caracteres.");

        return erros.ToArray();
    }
}