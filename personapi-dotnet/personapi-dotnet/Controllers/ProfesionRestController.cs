using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProfesionRestController : Controller
	{
		private readonly IProfesionRepository _profesionRepo;

		public ProfesionRestController(IProfesionRepository profesionRepo)
		{
			_profesionRepo = profesionRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
		{
			var profesiones = await _profesionRepo.GetAllAsync();
			return Ok(profesiones);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Profesion>> GetById(int id)
		{
			var profesion = await _profesionRepo.GetByIdAsync(id);
			if (profesion == null)
				return NotFound();
			return Ok(profesion);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Profesion profesion)
		{
			await _profesionRepo.AddAsync(profesion);
			await _profesionRepo.SaveAsync();
			return CreatedAtAction(nameof(GetById), new { id = profesion.Id }, profesion);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Profesion profesion)
		{
			if (id != profesion.Id)
				return BadRequest();

			_profesionRepo.Update(profesion);
			await _profesionRepo.SaveAsync();
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var profesion = await _profesionRepo.GetByIdAsync(id);
			if (profesion == null)
				return NotFound();

			_profesionRepo.Delete(profesion);
			await _profesionRepo.SaveAsync();
			return NoContent();
		}
	}
}
