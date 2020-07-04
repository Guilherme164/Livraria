using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        [Display(Name = "Data de Nascimento")]
        public DateTime Dt_Nascimmento { get; set; }
        public int Flag { get; set; }
        public ICollection<Nota> Nota { get; set; } = new List<Nota>();

        public Cliente()
        {

        }
    }
}
