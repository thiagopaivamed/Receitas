using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Models
{
    public class Receita
    {
        public int ReceitaId { get; set; }

        public string Nome { get; set; }
        
        [DataType(DataType.Upload)]
        public string Foto { get; set; }

        public double Preco { get; set; }

        public ICollection<Passo> Passos { get; set; }

    }
}
