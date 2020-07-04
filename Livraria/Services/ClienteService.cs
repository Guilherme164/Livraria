using Livraria.Data;
using Livraria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Services
{
    public class ClienteService
    {
        private readonly LivrariaContext _context;

        public ClienteService(LivrariaContext context)
        {
            _context = context;
        }
        public List<Cliente> FindAll()
        {
            return _context.Cliente.OrderBy(dep => dep.Id).ToList();
        }
        public Cliente FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(seller => seller.Id==id);
            return _context.Cliente.FirstOrDefault(cliente => cliente.Id == id);
        }
        public void Insert(Cliente cliente)
        {
            //Atribui o primeiro registro de departamento para o vendedor

            // sl.Department = _context.Department.First();
            _context.Add(cliente);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            var cliente = _context.Cliente.Find(id);
            //removendo
            _context.Cliente.Remove(cliente);
            //tipo um commit no banco
            _context.SaveChanges();
        }
        public void Update(Cliente cliente)
        {
            if (!_context.Cliente.Any(s => s.Id == cliente.Id))
            {
                Console.WriteLine("Id not found");
            }
            _context.Update(cliente);
            _context.SaveChanges();

        }
    }
}
