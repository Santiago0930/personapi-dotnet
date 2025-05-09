using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	public class ProfesionController : Controller
	{
		private readonly IProfesionRepository _profesionRepo;

		public ProfesionController(IProfesionRepository profesionRepo)
		{
			_profesionRepo = profesionRepo;
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
				_profesionRepo.Delete(profesion);
				await _profesionRepo.SaveAsync();
			}
			return RedirectToAction(nameof(Index));
		}

	}
}
