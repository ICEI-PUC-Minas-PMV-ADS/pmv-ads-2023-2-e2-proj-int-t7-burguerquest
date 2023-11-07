using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Puc.Models
{
	[Table("Produtos")]
	public class Produto
	{
		[Key]
		public int ProdutoId { get; set; }

		[Display(Name = "Nome do Lanche")]
		public string NomeProduto { get; set; }

        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "Url")]
        [StringLength(30)]
        public string Imageurl { get; set; }

    }
}

