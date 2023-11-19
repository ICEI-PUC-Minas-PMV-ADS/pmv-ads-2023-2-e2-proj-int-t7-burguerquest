using Puc.Models;

public class CarrinhoViewModel
{
    public List<Tuple<Produto, int>> ProdutosComIndices { get; set; }
    public Usuario Usuario { get; set; }
}
