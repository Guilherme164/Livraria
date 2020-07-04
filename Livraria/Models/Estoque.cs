using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Estoque
    {
        public int Id { get; set; }
        [Display(Name = "Estoque")]
        public string Nome_estoque { get; set; }
        //verificar
        [Display(Name = "Responsável")]
        public string Responsavel { get; set; }
        public ICollection<Livro> Livro { get; set; } = new List<Livro>();
        public Estoque()
        {

        }
    }
}
