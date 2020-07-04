using Livraria.Data;
using Livraria.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Services
{
    public class LivroService
    {
        private readonly LivrariaContext _context;

        public LivroService(LivrariaContext context)
        {
            _context = context;
        }
        public List<Livro> FindAll()
        {
            return _context.Livro.Include(estoque => estoque.Estoque).OrderBy(dep => dep.Titulo).ToList();
        }
        public Boolean FindByTitle(string word)
        {
            try
            {
               Livro li = _context.Livro.FirstOrDefault(livro => livro.Titulo == word);
                string x = li.Titulo;
                return true;
            }
            catch
            {
                return false;
            }

        }
        public int Count(string word)
        {
            try
            {
                int qtd = _context.Livro.Where(livro => livro.Disponibilidade == "Disponivel").Count(livro => livro.Titulo == word);
                return qtd;
            }
            catch
            {
                int erro = 0;
                return erro;
            }
        }
        public string SearchMet(string word)
        {
            try
            {
                Livro l = _context.Livro.FirstOrDefault(livro => livro.Disponibilidade == "Disponivel" && livro.Titulo == word);
                string titulo = l.Titulo;
                return titulo;
            }
            catch
            {
                string erro = "404 - algo deu errado!";
                return erro;
            }
        }
        public void Update_indisponivel(int id)
        {
            Livro livro = _context.Livro.FirstOrDefault(l => l.Id == id);
            if (!_context.Livro.Any(s => s.Id == livro.Id))
            {
                Console.WriteLine("Id not found");
            }
            livro.Disponibilidade = "Indisponivel";
            _context.Update(livro);
            _context.SaveChanges();

        }
        public int SearchMetId(string word)
        {
            try
            {
                Livro l = _context.Livro.FirstOrDefault(livro => livro.Disponibilidade == "Disponivel" && livro.Titulo == word);
                int id = l.Id;
                return id;
            }
            catch
            {
                //erros vao ser tratado pelo 2;
                int erro = 10000000;
                return erro;
            }
        }
        public Livro FindById(int id)
        {
            //return _context.Seller.FirstOrDefault(seller => seller.Id==id);
            return _context.Livro.FirstOrDefault(livro => livro.Id == id);
        }
        public List<Livro> FindByWord(string word)
        {
            //adicionar ViewData[] retornar o count

           return _context.Livro.Where(x => x.Titulo == word).ToList();
            
        }
        public void Insert(Livro livro)
        {
            _context.Add(livro);
            _context.SaveChanges();
        }
        public void Remove(int id)
        {
            var livro = _context.Livro.Find(id);
            //removendo
            _context.Livro.Remove(livro);
           
            _context.SaveChanges();
        }
        public void Update(Livro livro)
        {
            if (!_context.Livro.Any(s => s.Id == livro.Id))
            {
                Console.WriteLine("Id not found");
            }
            _context.Update(livro);
            _context.SaveChanges();

        }
        
    }
}
