using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	public class TelefonoController : Controller
	{
		private readonly ITelefonoRepository _telefonoRepo;
		private readonly IPersonaRepository _personaRepo;

		public TelefonoController(ITelefonoRepository telefonoRepo, IPersonaRepository personaRepo)
		{
			_telefonoRepo = telefonoRepo;
			_personaRepo = personaRepo;
		}

		public async Task<IActionResult> Index()
		{
			var telefonos = await _telefonoRepo.GetAllAsync();
			return View(telefonos);
		}

		public async Task<IActionResult> Create()
		{
			ViewBag.Personas = await _personaRepo.GetAllAsync();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Telefono telefono)
		{
			var existente = await _telefonoRepo.GetByIdAsync(telefono.Num);
			if (existente != null)
			{
				ModelState.AddModelError(string.Empty, "Ya existe un teléfono con este número.");
				ViewBag.Personas = await _personaRepo.GetAllAsync();
				return View(telefono);
			}

			var telefonos = await _telefonoRepo.GetAllAsync();
			if (telefonos.Any(t => t.Duenio == telefono.Duenio))
			{
				ModelState.AddModelError(string.Empty, "Esta persona ya tiene un número de teléfono asignado.");
				ViewBag.Personas = await _personaRepo.GetAllAsync();
				return View(telefono);
			}

			if (ModelState.IsValid)
			{
				await _telefonoRepo.AddAsync(telefono);
				await _telefonoRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Personas = await _personaRepo.GetAllAsync();
			return View(telefono);
		}



		public async Task<IActionResult> Edit(string num)
		{
			var telefono = await _telefonoRepo.GetByIdAsync(num);
			if (telefono == null) return NotFound();

			ViewBag.Personas = await _personaRepo.GetAllAsync();
			return View(telefono);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string num, Telefono telefono)
		{
			if (num != telefono.Num) return BadRequest();

			if (ModelState.IsValid)
			{
				_telefonoRepo.Update(telefono);
				await _telefonoRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Personas = await _personaRepo.GetAllAsync();
			return View(telefono);
		}

		public async Task<IActionResult> Details(string num)
		{
			var telefono = await _telefonoRepo.GetByIdAsync(num);
			if (telefono == null) return NotFound();

			return View(telefono);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string num)
		{
			var telefono = await _telefonoRepo.GetByIdAsync(num);
			if (telefono != null)
			{
				_telefonoRepo.Delete(telefono);
				await _telefonoRepo.SaveAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
