using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Passo> Passos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
