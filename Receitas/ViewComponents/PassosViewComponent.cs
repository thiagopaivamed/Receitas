using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Receitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Receitas.ViewComponents
{
    public class PassosViewComponent : ViewComponent
    {
        private readonly Contexto _contexto;

        public PassosViewComponent(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IViewComponentResult> InvokeAsync(int receitaId)
        {
            return View(await _contexto.Passos.Where(p => p.ReceitaId == receitaId).ToListAsync());
        }
    }
}
