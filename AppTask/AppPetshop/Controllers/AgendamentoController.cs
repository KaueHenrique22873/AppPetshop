using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppPetshop.Models;

namespace AppPetshop.Controllers
{
    public class AgendamentoController : Controller
    {
        private readonly PetshopContext _context;

        public AgendamentoController(PetshopContext context)
        {
            _context = context;
        }

        // GET: Agendamento
        public async Task<IActionResult> Index()
        {
            var petshopContext = _context.Agendamentos.Include(a => a.IdServicoNavigation).Include(a => a.IdTutorNavigation);
            return View(await petshopContext.ToListAsync());
        }

        // GET: Agendamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.IdServicoNavigation)
                .Include(a => a.IdTutorNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamento/Create
        public IActionResult Create()
        {
            ViewData["IdServico"] = new SelectList(_context.Servicos, "Codigo", "Codigo");
            ViewData["IdTutor"] = new SelectList(_context.Tutors, "Codigo", "Codigo");
            return View();
        }

        // POST: Agendamento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Datahora,IdTutor,IdServico")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServico"] = new SelectList(_context.Servicos, "Codigo", "Codigo", agendamento.IdServico);
            ViewData["IdTutor"] = new SelectList(_context.Tutors, "Codigo", "Codigo", agendamento.IdTutor);
            return View(agendamento);
        }

        // GET: Agendamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["IdServico"] = new SelectList(_context.Servicos, "Codigo", "Codigo", agendamento.IdServico);
            ViewData["IdTutor"] = new SelectList(_context.Tutors, "Codigo", "Codigo", agendamento.IdTutor);
            return View(agendamento);
        }

        // POST: Agendamento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Datahora,IdTutor,IdServico")] Agendamento agendamento)
        {
            if (id != agendamento.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdServico"] = new SelectList(_context.Servicos, "Codigo", "Codigo", agendamento.IdServico);
            ViewData["IdTutor"] = new SelectList(_context.Tutors, "Codigo", "Codigo", agendamento.IdTutor);
            return View(agendamento);
        }

        // GET: Agendamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
                .Include(a => a.IdServicoNavigation)
                .Include(a => a.IdTutorNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
            return _context.Agendamentos.Any(e => e.Codigo == id);
        }
    }
}
