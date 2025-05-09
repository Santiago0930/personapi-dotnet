﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	public class PersonaController : Controller
	{
		private readonly IPersonaRepository _personaRepo;
		private readonly persona_dbContext _context;

		public PersonaController(IPersonaRepository personaRepo, persona_dbContext context)
		{
			_personaRepo = personaRepo;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var personas = await _personaRepo.GetAllAsync();
			return View(personas);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Persona persona)
		{
			var existente = await _personaRepo.GetByIdAsync(persona.Cc);
			if (existente != null)
			{
				ModelState.AddModelError(string.Empty, "Ya existe una persona registrada con esta cédula.");
				return View(persona);
			}

			if (ModelState.IsValid)
			{
				await _personaRepo.AddAsync(persona);
				await _personaRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(persona);
		}

		public async Task<IActionResult> Edit(int id)
		{
			var persona = await _personaRepo.GetByIdAsync(id);
			if (persona == null)
				return NotFound();

			return View(persona);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Persona persona)
		{
			if (id != persona.Cc)
				return NotFound();

			if (ModelState.IsValid)
			{
				_personaRepo.Update(persona);
				await _personaRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(persona);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var persona = await _personaRepo.GetByIdAsync(id);
			return View(persona);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var persona = await _personaRepo.GetByIdAsync(id);

			if (persona == null)
				return NotFound();

			var estudios = await _context.Estudios.Where(e => e.CcPer == id).ToListAsync();
			_context.Estudios.RemoveRange(estudios);

			var telefonos = await _context.Telefonos.Where(t => t.Duenio == id).ToListAsync();
			_context.Telefonos.RemoveRange(telefonos);

			_personaRepo.Delete(persona);
			await _personaRepo.SaveAsync();

			return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> Details(int id)
		{
			var persona = await _personaRepo.GetByIdAsync(id);
			if (persona == null)
				return NotFound();

			return View(persona);
		}
	}
}
