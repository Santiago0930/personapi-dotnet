using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PersonaRestController : Controller
	{
		private readonly IPersonaRepository _personaRepo;

		public PersonaRestController(IPersonaRepository personaRepo)
		{
			_personaRepo = personaRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
		{
			var personas = await _personaRepo.GetAllAsync();
			return Ok(personas);
		}

		[HttpGet("{cc}")]
		public async Task<ActionResult<Persona>> GetById(int cc)
		{
			var persona = await _personaRepo.GetByIdAsync(cc);
			if (persona == null)
				return NotFound();
			return Ok(persona);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Persona persona)
		{
			await _personaRepo.AddAsync(persona);
			await _personaRepo.SaveAsync();
			return CreatedAtAction(nameof(GetById), new { cc = persona.Cc }, persona);
		}

		[HttpPut("{cc}")]
		public async Task<ActionResult> Update(int cc, Persona persona)
		{
			if (cc != persona.Cc)
				return BadRequest();

			_personaRepo.Update(persona);
			await _personaRepo.SaveAsync();
			return NoContent();
		}

		[HttpDelete("{cc}")]
		public async Task<ActionResult> Delete(int cc)
		{
			var persona = await _personaRepo.GetByIdAsync(cc);
			if (persona == null)
				return NotFound();

			_personaRepo.Delete(persona);
			await _personaRepo.SaveAsync();
			return NoContent();
		}
	}
}
