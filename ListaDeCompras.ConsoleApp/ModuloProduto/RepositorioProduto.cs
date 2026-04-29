using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.ModuloCategoria;

namespace ListaDeCompras.ConsoleApp.ModuloProduto;

public class RepositorioProduto : RepositorioBase<Produto>
{
    public bool ExisteProdutoComMesmoNomeNaCategoria(string nome, Categoria categoria, string idIgnorado = "")
    {
        foreach (Produto produto in registros)
        {
            bool mesmoNome = produto.Nome.Trim().Equals(nome.Trim(), StringComparison.OrdinalIgnoreCase);
            bool mesmaCategoria = produto.Categoria.Id == categoria.Id;
            bool mesmoRegistro = produto.Id == idIgnorado;

            if (mesmoNome && mesmaCategoria && !mesmoRegistro)
                return true;
        }

        return false;
    }
}