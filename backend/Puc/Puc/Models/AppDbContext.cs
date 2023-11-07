using System;
using Puc.Controllers;
using Microsoft.EntityFrameworkCore;
namespace Puc.Models
{
	public class AppDbContext : DbContext
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Produto> Produtos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

    }
}

