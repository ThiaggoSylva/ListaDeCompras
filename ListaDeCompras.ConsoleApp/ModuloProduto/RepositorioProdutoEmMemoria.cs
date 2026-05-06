using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Compartilhado.Memoria;

namespace ListaDeCompras.ConsoleApp.ModuloProduto;

public class RepositorioProdutoEmMemoria : RepositorioBaseEmMemoria<Produto>, IRepositorio<Produto>;