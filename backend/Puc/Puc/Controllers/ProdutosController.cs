using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Puc.Models;

namespace Puc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private List<int> carrinho = new List<int>(); // Armazena índices dos itens no carrinho

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }


        //tentativa de lista dos lanches

        public async Task<IActionResult> List()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return View(produtos);
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            return _context.Produtos != null ?
                        View(await _context.Produtos.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Produtos'  is null.");
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,NomeProduto,Preco,Imageurl")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,NomeProduto,Preco,Imageurl")] Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'AppDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return (_context.Produtos?.Any(e => e.ProdutoId == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> CarrinhoAsync()
        {
            var carrinho = HttpContext.Session.Get<List<int>>("Carrinho") ?? new List<int>();

            var produtosNoCarrinho = new List<Produto>();
            foreach (var indice in carrinho)
            {
                var produto = _context.Produtos.ToList()[indice]; // Busca o produto pelo índice
                produtosNoCarrinho.Add(produto);
            }

            var usuarioLogado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nome == User.Identity.Name);

            var model = new Tuple<List<Produto>, Usuario>(produtosNoCarrinho, usuarioLogado);

            return View(model);
        }

        public IActionResult AdicionarAoCarrinho(int indice)
        {
            var carrinho = HttpContext.Session.Get<List<int>>("Carrinho") ?? new List<int>();
            carrinho.Add(indice);
            HttpContext.Session.Set("Carrinho", carrinho);

            return RedirectToAction(nameof(List));
        }
        public IActionResult RemoverDoCarrinho(int produtoId)
        {
            var carrinho = HttpContext.Session.Get<List<int>>("Carrinho") ?? new List<int>();

            // Verifica se o produtoId está presente no carrinho e remove se encontrado
            if (carrinho.Contains(produtoId))
            {
                carrinho.Remove(produtoId);
                HttpContext.Session.Set("Carrinho", carrinho);
            }

            // Redireciona para a mesma página (CarrinhoAsync) para atualizar a exibição

            return RedirectToAction("Carrinho", "Produtos");
        }


    }
}
