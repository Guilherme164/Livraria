using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.Models;

namespace Livraria.Data
{
    public class LivrariaContext : DbContext
    {
        public LivrariaContext (DbContextOptions<LivrariaContext> options)
            : base(options)
        {
        }
        public DbSet<Livraria.Models.Livro> Livro { get; set; }
        public DbSet<Livraria.Models.Cliente> Cliente { get; set; }
        public DbSet<Livraria.Models.Estoque> Estoque { get; set; }
        public DbSet<Livraria.Models.Nota> Nota { get; set; }
    }
}
