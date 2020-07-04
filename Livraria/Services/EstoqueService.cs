using Livraria.Data;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Services
{
    public class EstoqueService
    {
        private readonly LivrariaContext _context;
        
        public EstoqueService(LivrariaContext context)
        {
            _context = context;
        }
        public List<Estoque> FindAll()
        {
            return _context.Estoque.OrderBy(dep => dep.Id).ToList();
        }
        public Estoque FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(seller => seller.Id==id);
            return _context.Estoque.FirstOrDefault(estoque => estoque.Id == id);
        }
        public void Insert(Estoque estoque)
        {
            //Atribui o primeiro registro de departamento para o vendedor

            // sl.Department = _context.Department.First();
            _context.Add(estoque);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            var estoque = _context.Estoque.Find(id);
            //removendo
            _context.Estoque.Remove(estoque);
            //tipo um commit no banco
            _context.SaveChanges();
        }
        public void Update(Estoque estoque)
        {
            if (!_context.Estoque.Any(s => s.Id == estoque.Id))
            {
                Console.WriteLine("Id not found");
            }
            _context.Update(estoque);
            _context.SaveChanges();

        }
        public List<Livro> BuscarNoEstoque(int id)
        {
            return _context.Livro.Where(livro => livro.EstoqueId == id).ToList();
        }
    }
}
