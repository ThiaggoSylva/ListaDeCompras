using ListaDeCompras.ConsoleApp.Compartilhado;
using ListaDeCompras.ConsoleApp.Compartilhado.Memoria;

namespace ListaDeCompras.ConsoleApp.ModuloListaCompras;

public class RepositorioListaComprasEmMemoria : RepositorioBaseEmMemoria<ListaCompras>, IRepositorio<ListaCompras>;