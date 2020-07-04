using Livraria.Data;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Services
{
    public class NotaService
    {
        private readonly LivrariaContext _context;
        public NotaService(LivrariaContext context)
        {
            _context = context;
        }
        public List<Nota> FindAll()
        {
            return _context.Nota.Include(livro=>livro.Livro).Include(cliente=>cliente.Cliente).OrderBy(dep => dep.Id).ToList();
        }
        public Nota FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(seller => seller.Id==id);
            return _context.Nota.Include(livro=>livro.Livro).Include(cliente=>cliente.Cliente).FirstOrDefault(nota => nota.Id == id);
        }
        public void Insert(Nota nota)
        {
            _context.Add(nota);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            var nota = _context.Nota.Find(id);
            //removendo
            _context.Nota.Remove(nota);
            //tipo um commit no banco
            _context.SaveChanges();
        }
        public void Update(Nota nota)
        {
            if (!_context.Nota.Any(s => s.Id == nota.Id))
            {
                Console.WriteLine("Id not found");
            }
            _context.Update(nota);
            _context.SaveChanges();

        }
        public List<Nota> Clientelivroslocados(Cliente cliente)
        {
            int ano = 0001;
            int mes = 01;
            int dia = 01;
            int hora = 00;
            int minuto = 00;
            int segundo = 00;
            DateTime d = new DateTime(ano, mes, dia, hora, minuto, segundo);
            //DateTime d = DateTime.Parse("01 / 01 / 0001 0001 00:00:00");
            //DateTime d = new DateTime('01 / 01 / 0001 00:00:00'; "DD-MMM-YYYY hh:mm:ss");  
            //return _context.Nota.Where(nota => nota.Data_devolucao == d && nota.Cliente == cliente).ToList();
            return _context.Nota.Include(livro => livro.Livro).Include(cli => cli.Cliente).OrderBy(dep => dep.Id).Where(nota => nota.Data_devolucao == d && nota.Cliente == cliente).ToList();
        }

    }
}
