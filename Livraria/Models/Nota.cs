using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Nota
    {
        public int Id { get; set; }
        [Display(Name = "Data de Retirada")]
        public DateTime Data_retirada { get; set; }

        [Display(Name = "Data de Devolução")]
        public DateTime Data_devolucao { get; set; }

        [Display(Name = "Data de Vencimento")]
        public DateTime Data_vencimento { get; set; }
        
        [Display(Name = "Data de Renovação")]
        public DateTime Data_renovacao { get; set; }
        
        [Display(Name = "Número de renovações")]
        public int Nr_renovacoes { get; set; }
        [Required]
        [Display(Name = "Serial do Livro")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        [Required]
        [Display(Name = "Matricula Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Nota()
        {
        }
    }
}
