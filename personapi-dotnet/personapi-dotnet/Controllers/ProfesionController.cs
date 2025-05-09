using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	public class ProfesionController : Controller
	{
		private readonly IProfesionRepository _profesionRepo;
		private readonly persona_dbContext _context;

		public ProfesionController(IProfesionRepository profesionRepo, persona_dbContext context)
		{
			_profesionRepo = profesionRepo;
			_context = context;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var profesiones = await _profesionRepo.GetAllAsync();
			return View(profesiones);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Profesion profesion)
		{
			var profesiones = await _profesionRepo.GetAllAsync();
			if (profesiones.Any(p => p.Nom != null && p.Nom.ToLower() == profesion.Nom?.ToLower()))
			{
				ModelState.AddModelError(string.Empty, "Ya existe una profesión registrada con ese nombre.");
				return View(profesion);
			}

			if (ModelState.IsValid)
			{
				await _profesionRepo.AddAsync(profesion);
				await _profesionRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(profesion);
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var profesion = await _profesionRepo.GetByIdAsync(id);
			if (profesion == null)
				return NotFound();
			return View(profesion);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Profesion profesion)
		{
			if (id != profesion.Id)
				return BadRequest();

			if (ModelState.IsValid)
			{
				_profesionRepo.Update(profesion);
				await _profesionRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(profesion);
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var profesion = await _profesionRepo.GetByIdAsync(id);
			if (profesion == null)
				return NotFound();
			return View(profesion);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			var profesion = await _profesionRepo.GetByIdAsync(id);
			if (profesion != null)
			{
				var estudios = _context.Estudios.Where(e => e.IdProf == id);
				_context.Estudios.RemoveRange(estudios);
				_profesionRepo.Delete(profesion);
				await _context.SaveChangesAsync();
			}
			return RedirectToAction(nameof(Index));
		}


	}
}
