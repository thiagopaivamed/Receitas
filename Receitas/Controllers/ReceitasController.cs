using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.Controllers
{
    public class ReceitasController : Controller
    {
        private readonly Contexto _contexto;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReceitasController(Contexto contexto, IWebHostEnvironment webHostEnvironment)
        {
            _contexto = contexto;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Receitas.ToListAsync());
        }

        [HttpGet]
        public IActionResult NovaReceita()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NovaReceita(Receita receita, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if(foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        receita.Foto = "~/img/" + nomeFoto;
                    }

                    await _contexto.Receitas.AddAsync(receita);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(receita);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarReceita(int receitaId)
        {
            Receita receita = await _contexto.Receitas.FindAsync(receitaId);
            TempData["FotoReceita"] = receita.Foto;
            return View(receita);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarReceita(Receita receita, IFormFile foto)
        {
            if (ModelState.IsValid)
            {
                if (foto != null)
                {
                    string diretorio = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                    string nomeFoto = Guid.NewGuid().ToString() + foto.FileName;

                    using (FileStream fileStream = new FileStream(Path.Combine(diretorio, nomeFoto), FileMode.Create))
                    {
                        await foto.CopyToAsync(fileStream);
                        receita.Foto = "~/img/" + nomeFoto;
                    }
                }
                else
                    receita.Foto = TempData["FotoReceita"].ToString();

                _contexto.Receitas.Update(receita);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(receita);
        }

        [HttpPost]
        public async Task<JsonResult> Excluir(int id)
        {
            Receita receita = await _contexto.Receitas.FindAsync(id);
            var passos = _contexto.Passos.Where(p => p.ReceitaId == id);

            if(await passos.CountAsync() > 0)
            {
                _contexto.Passos.RemoveRange(passos);
            }

            _contexto.Receitas.Remove(receita);
            await _contexto.SaveChangesAsync();

            return Json(new { });
        }

        public async Task<IActionResult> DetalhesReceita(int receitaId)
        {
            Receita receita = await _contexto.Receitas.FindAsync(receitaId);

            return View(receita);
        }
    }
}
