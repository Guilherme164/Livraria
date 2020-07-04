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
using Microsoft.AspNetCore.Http;

namespace Livraria.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService context)
        {
            _clienteService = context;
        }

        // GET: Clientes
        public IActionResult Index()
        {
            var list = _clienteService.FindAll();

            return View(list);
        }

        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {

            _clienteService.Insert(cliente);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var seller = _clienteService.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);

        }
        [HttpGet]
        public List<Cliente> BuscarClientesId(int id)
        {
            List<Cliente> clientes = _clienteService.FindAll();
            return clientes;
        }

        /*public JsonResult BuscarClienteId(FormCollection form)
        {
            var id = form['ClienteId'];

            var user =_clienteService.FindById(id);
            return Json(user);
            
        }*/

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            _clienteService.Remove(Id);
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //se for valido o ID
            var cliente = _clienteService.FindById(id.Value);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Cliente cliente)
        {
            /*Verificar se realmente é sellerformviewmodel e n seller*/
            if (cliente.Id != id)
            {
                return NotFound();
            }

            //Essa chamada de update pode gerar duas exceções, sempre lembre das importações 
            _clienteService.Update(cliente);
            return RedirectToAction(nameof(Index));

        }



    }
      
}
