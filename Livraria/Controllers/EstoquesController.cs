using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.Data;
using Livraria.Models;
using Livraria.Services;


namespace Livraria.Controllers
{
    public class EstoquesController : Controller
    {
        private readonly EstoqueService _estoqueService;

        public EstoquesController(EstoqueService context)
        {
            _estoqueService = context;
        }

        // GET: Clientes
        public IActionResult Index()
        {
            var list = _estoqueService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Estoque estoque)
        {

            _estoqueService.Insert(estoque);
            return RedirectToAction("Index");
        }
         public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var estoque = _estoqueService.FindById(id.Value);
            if (estoque == null)
            {
                return NotFound();
            }

            return View(estoque);

        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            _estoqueService.Remove(Id);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var estoque = _estoqueService.FindById(id.Value);
            if (estoque == null)
            {
                return NotFound();
            }

            return View(estoque);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Estoque estoque)
        {
            /*Verificar se realmente é sellerformviewmodel e n seller*/
            if (estoque.Id != id)
            {
                return NotFound();
            }

            //Essa chamada de update pode gerar duas exceções, sempre lembre das importações 
            _estoqueService.Update(estoque);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult BuscarLivros(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var estoque = _estoqueService.FindById(id.Value);
            if (estoque == null)
            {
                return NotFound();
            }
            List<Livro> livros = _estoqueService.BuscarNoEstoque(id.Value);

            return View(livros);
        }

    }
}
