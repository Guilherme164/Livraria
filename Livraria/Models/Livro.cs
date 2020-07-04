using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string Autor { get; set; }
        public string Disponibilidade { get; set; }

        public int EstoqueId { get; set; }
        public Estoque Estoque { get; set; }

        public ICollection<Nota> Nota { get; set; } = new List<Nota>();
        
        public Livro()
        {

        }
    }
}
