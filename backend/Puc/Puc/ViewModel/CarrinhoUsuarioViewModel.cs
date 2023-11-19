namespace Puc.Models
{
    public class CarrinhoUsuarioViewModel
    {
        public Usuario Usuario { get; set; }
        public List<Produto> ItensNoCarrinho { get; set; }
    }
}