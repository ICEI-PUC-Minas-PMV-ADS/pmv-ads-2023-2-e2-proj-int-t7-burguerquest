using Microsoft.AspNetCore.Mvc;
using Puc.Models;
using System.Collections.Generic;
using System.Linq;

namespace Puc.Controllers
{
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }

        // Action para finalizar o pedido
        public IActionResult FinalizarPedido()
        {
            var usuario = ObterUsuarioLogado(); // Implemente a lógica para obter o usuário logado
            var produtosNoCarrinho = ObterProdutosNoCarrinho(); // Implemente a lógica para obter os produtos no carrinho

            var viewModel = new CarrinhoUsuarioViewModel
            {
                Usuario = usuario,
                ItensNoCarrinho = produtosNoCarrinho
            };

            return View(viewModel); // Retorna a view com o modelo CarrinhoUsuarioViewModel
        }

        // Outros métodos do controller...

        // Método para obter o usuário logado (exemplo simples, ajuste conforme sua aplicação)
        private Usuario ObterUsuarioLogado()
        {
            // Lógica para obter o usuário logado
            return _context.Usuarios.FirstOrDefault(); // Exemplo: obtendo o primeiro usuário do banco de dados
        }

        // Método para obter os produtos no carrinho (exemplo simples, ajuste conforme sua aplicação)
        private List<Produto> ObterProdutosNoCarrinho()
        {
            // Lógica para obter os produtos no carrinho
            return _context.Produtos.ToList(); // Exemplo: obtendo todos os produtos do banco de dados
        }
    }
}
