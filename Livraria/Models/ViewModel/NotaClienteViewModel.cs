using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models.ViewModel
{
    public class NotaClienteViewModel
    {
        public Nota Nota { get; set; }
        public ICollection<Cliente> Cliente { get; set; }
        //public Livro Livro { get; set; }
    }
}
