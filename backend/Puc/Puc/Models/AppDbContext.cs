using System;
using Puc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Puc.Models
{
	public class AppDbContext : IdentityDbContext<IdentityUser>
	{

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Produto> Produtos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

       
    }
}

