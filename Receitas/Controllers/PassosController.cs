using Microsoft.AspNetCore.Mvc;
using Receitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Controllers
{
    public class PassosController : Controller
    {
        private readonly Contexto _contexto;

        public PassosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult NovoPasso(int receitaId)
        {
            Passo passo = new Passo
            {
                ReceitaId = receitaId
            };
            return View(passo);
        }

        [HttpPost]
        public async Task<IActionResult> NovoPasso(Passo passo)
        {
            await _contexto.Passos.AddAsync(passo);
            await _contexto.SaveChangesAsync();

            return RedirectToAction("DetalhesReceita", "Receitas", new { receitaId = passo.ReceitaId });
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarPasso(int passoId)
        {
            Passo passo = await _contexto.Passos.FindAsync(passoId);
            return View(passo);
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarPasso(Passo passo)
        {
            _contexto.Passos.Update(passo);
            await _contexto.SaveChangesAsync();
            return RedirectToAction("DetalhesReceita", "Receitas", new { receitaId = passo.ReceitaId });
        }

        [HttpPost]
        public async Task<JsonResult> Excluir(int id)
        {
            Passo passo = await _contexto.Passos.FindAsync(id);
            _contexto.Passos.Remove(passo);
            await _contexto.SaveChangesAsync();

            return Json(new { });
        }
    }
}
