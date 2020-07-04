using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models.ViewModel
{
    public class EstoqueLivrosViewModel
    {
        public Livro Livro { get; set; }
        public ICollection<Estoque> Estoque { get; set; }
    }
}
