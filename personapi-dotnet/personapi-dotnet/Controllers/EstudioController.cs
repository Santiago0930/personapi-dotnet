using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	public class EstudioController : Controller
	{
		private readonly IEstudioRepository _estudioRepo;
		private readonly IPersonaRepository _personaRepo;
		private readonly IProfesionRepository _profesionRepo;
		private readonly persona_dbContext _context; // agregado para el Include

		public EstudioController(
			IEstudioRepository estudioRepo,
			IPersonaRepository personaRepo,
			IProfesionRepository profesionRepo,
			persona_dbContext context)
		{
			_estudioRepo = estudioRepo;
			_personaRepo = personaRepo;
			_profesionRepo = profesionRepo;
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			var estudios = await _context.Estudios
				.Include(e => e.CcPerNavigation)
				.Include(e => e.IdProfNavigation)
				.ToListAsync();

			return View(estudios);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			ViewBag.Personas = await _personaRepo.GetAllAsync();
			ViewBag.Profesiones = await _profesionRepo.GetAllAsync();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Estudio estudio)
		{
			var existente = await _estudioRepo.GetByIdsAsync(estudio.CcPer, estudio.IdProf);

			if (existente != null)
			{
				ModelState.AddModelError(string.Empty, "Este estudio ya existe.");
				ViewBag.Personas = await _personaRepo.GetAllAsync();
				ViewBag.Profesiones = await _profesionRepo.GetAllAsync();
				return View(estudio);
			}

			if (ModelState.IsValid)
			{
				await _estudioRepo.AddAsync(estudio);
				await _estudioRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Personas = await _personaRepo.GetAllAsync();
			ViewBag.Profesiones = await _profesionRepo.GetAllAsync();
			return View(estudio);
		}

		public async Task<IActionResult> Edit(int ccPer, int idProf)
		{
			var estudio = await _estudioRepo.GetByIdsAsync(ccPer, idProf);
			if (estudio == null)
				return NotFound();

			return View(estudio);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(Estudio estudio)
		{
			if (ModelState.IsValid)
			{
				_estudioRepo.Update(estudio);
				await _estudioRepo.SaveAsync();
				return RedirectToAction(nameof(Index));
			}

			return View(estudio);
		}

		public async Task<IActionResult> Details(int ccPer, int idProf)
		{
			var estudio = await _context.Estudios
				.Include(e => e.CcPerNavigation)
				.Include(e => e.IdProfNavigation)
				.FirstOrDefaultAsync(e => e.CcPer == ccPer && e.IdProf == idProf);

			if (estudio == null)
				return NotFound();

			return View(estudio);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int ccPer, int idProf)
		{
			var estudio = await _estudioRepo.GetByIdsAsync(ccPer, idProf);
			if (estudio != null)
			{
				_estudioRepo.Delete(estudio);
				await _estudioRepo.SaveAsync();
			}
			return RedirectToAction(nameof(Index));
		}
	}
}
