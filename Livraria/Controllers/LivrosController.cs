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
    public class LivrosController : Controller
    {
            private readonly LivroService _livroService;
            private readonly EstoqueService _estoqueService;
            private readonly ClienteService _clienteService;
        //teste
        public LivrosController(LivroService livro_services, EstoqueService estoque_services, ClienteService cliente_service)
            {
                _livroService = livro_services;
                _estoqueService = estoque_services;
                _clienteService = cliente_service;
            }

            // GET: Clientes
            public IActionResult Index()
            {
                var list = _livroService.FindAll();

                return View(list);
            }

            public IActionResult Create()
            {
            /*string word = "teste 12314124";
            var livros  = _livroService.FindByWord(word);*/
            
            var list = _estoqueService.FindAll();

            var viewModel = new EstoqueLivrosViewModel{Estoque = list};

                return View(viewModel);
            }

            [HttpGet]
            public IActionResult Search(string word)
            {
            int teste_contador = _livroService.Count(word);
            Boolean teste_livro = _livroService.FindByTitle(word);
            int teste_id = _livroService.SearchMetId(word);
            string teste_titulo = _livroService.SearchMet(word);
            
            if (teste_id != 0 && teste_titulo != "404 - algo deu errado!" && teste_contador != 10000000)
            {
                ViewData["Qtd"] = _livroService.Count(word);
                ViewData["Titulo"] = _livroService.SearchMet(word);
                ViewData["Id"] = _livroService.SearchMetId(word);
                //var list =_livroService.FindByWord(word);
                List<Cliente> list2 = _clienteService.FindAll();
                var viewModel = new NotaClienteViewModel { Cliente = list2 };

                return View(viewModel);
            }
            else if (teste_contador == 0 && teste_livro==true)
            {
                return RedirectToAction("LivroEsgotado");
            }
            else if (teste_titulo == "404 - algo deu errado!")
            {
                return RedirectToAction("TituloErrado");
            }
            else if(teste_id== 10000000)
            {
                return RedirectToAction("LivroNaoEncontrado");
            }
            else
            {
                    return RedirectToAction("ErroDePesquisa");

            }
            }
        public IActionResult ErroDePesquisa(string word)
        {
            return View();
        }
        public IActionResult TituloErrado(string word)
        {
            return View();
        }
        public IActionResult LivroEsgotado(string word)
        {
            return View();
        }
        public IActionResult LivroNaoEncontrado(string word)
        {
            return View();
        }


        [HttpPost]
            public IActionResult Create(Livro livro)
            {
            livro.Disponibilidade = "Disponivel";
                _livroService.Insert(livro);
                return RedirectToAction("Index");
            }
            public IActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                //se for valido o ID
                var livro = _livroService.FindById(id.Value);
                if (livro == null)
                {
                    return NotFound();
                }

                return View(livro);

            }

            [HttpPost]
            public IActionResult Delete(int Id)
            {
                _livroService.Remove(Id);
                return RedirectToAction(nameof(Index));

            }

            public IActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }
                //se for valido o ID
                var livro = _livroService.FindById(id.Value);
                if (livro == null)
                {
                    return NotFound();
                }
            List<Estoque> estoque = _estoqueService.FindAll();
            EstoqueLivrosViewModel viewModel = new EstoqueLivrosViewModel { Livro = livro, Estoque = estoque };

            return View(viewModel);
            }

            [HttpPost]
            public IActionResult Edit(int? id, Livro livro)
            {
                /*Verificar se realmente é sellerformviewmodel e n seller*/
                if (livro.Id != id)
                {
                    return NotFound();
                }

                //Essa chamada de update pode gerar duas exceções, sempre lembre das importações 
                _livroService.Update(livro);
                return RedirectToAction(nameof(Index));

            }
            public IActionResult Adicionarlivro(int? id)
            {
           
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var livro = _livroService.FindById(id.Value);
            if (livro == null)
            {
                return NotFound();
            }
            Livro novoLivro = new Livro();

            novoLivro.Autor = livro.Autor;
            novoLivro.Titulo = livro.Titulo;
            novoLivro.EstoqueId = livro.EstoqueId;
            novoLivro.Editora = livro.Editora;
            novoLivro.Disponibilidade = "Disponivel";
            novoLivro.Id = 0;
            
            _livroService.Insert(novoLivro);
         
                return RedirectToAction(nameof(Index));

            }

    }
}
