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
using Livraria.Models.ViewModel;

namespace Livraria.Controllers
{
    public class NotasController : Controller
    {
        private readonly LivroService _livroService;
        private readonly NotaService _notaService;
        private readonly ClienteService _clienteService;

        public NotasController(LivroService livro_services, NotaService notaService, ClienteService clienteService)
        {
            _livroService = livro_services;
            _notaService = notaService;
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            var list = _notaService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
            var list = _livroService.FindAll();
            var list2 = _clienteService.FindAll();
            var viewModel = new NotaViewModel { Livro = list, Cliente = list2 };

            return View(viewModel);
        }
       
        [HttpPost]
        public IActionResult Alocate(Nota nota)
        {
            nota.Data_retirada = DateTime.Today;
            nota.Data_vencimento = nota.Data_retirada.AddDays(7);
            nota.Nr_renovacoes = 0;
            _notaService.Insert(nota);
            int id = nota.LivroId;
            _livroService.Update_indisponivel(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Nota nota)
        {
            nota.Data_retirada = DateTime.Today;

            nota.Data_vencimento = nota.Data_retirada.AddDays(7);
            nota.Nr_renovacoes = 0;

            _notaService.Insert(nota);
            int id = nota.LivroId;
            _livroService.Update_indisponivel(id);

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var nota = _notaService.FindById(id.Value);
            if (nota == null)
            {
                return NotFound();
            }

            return View(nota);

        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            _notaService.Remove(Id);
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var nota = _notaService.FindById(id.Value);
            if (nota == null)
            {
                return NotFound();
            }
            List<Livro> livro = _livroService.FindAll();
            List<Cliente> cliente = _clienteService.FindAll();
            NotaViewModel viewModel = new NotaViewModel { Nota=nota, Livro = livro, Cliente = cliente };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Nota nota)
        {
            /*Verificar se realmente é sellerformviewmodel e n seller*/
            if (nota.Id != id)
            {
                return NotFound();
            }

            //Essa chamada de update pode gerar duas exceções, sempre lembre das importações 
            _notaService.Update(nota);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Renovar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            Nota nota = _notaService.FindById(id.Value);
            if (nota == null)
            {
                return NotFound();
            }

            int id_livro = nota.LivroId;

            if (nota.Nr_renovacoes < 8) {
                nota.Nr_renovacoes = nota.Nr_renovacoes + 1;
                nota.Data_renovacao = DateTime.Now;
                nota.Data_vencimento = nota.Data_renovacao.AddDays(7);
                _notaService.Update(nota);
            }
           

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Desalocar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            Nota nota = _notaService.FindById(id.Value);
            if (nota == null)
            {
                return NotFound();
            }
            nota.Data_devolucao = DateTime.Now;
            _notaService.Update(nota);
            int id_livro = nota.LivroId;

            Livro livro = _livroService.FindById(id_livro);

            livro.Disponibilidade = "Disponivel";

            _livroService.Update(livro);
          
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Relatorio(int matricula)
        {
            Cliente cliente = _clienteService.FindById(matricula);
            List<Nota> notas = _notaService.Clientelivroslocados(cliente); 
            return View(notas);
        }

    }
}
