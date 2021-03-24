using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Models
{
    public class Passo
    {
        public int PassoId { get; set; }

        public string Descricao { get; set; }

        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
    }
}
