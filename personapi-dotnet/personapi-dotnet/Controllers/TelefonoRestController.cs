using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TelefonoRestController : Controller
	{
		private readonly ITelefonoRepository _telefonoRepo;

		public TelefonoRestController(ITelefonoRepository telefonoRepo)
		{
			_telefonoRepo = telefonoRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
		{
			var telefonos = await _telefonoRepo.GetAllAsync();
			return Ok(telefonos);
		}

		[HttpGet("{num}")]
		public async Task<ActionResult<Telefono>> GetById(string num)
		{
			var telefono = await _telefonoRepo.GetByIdAsync(num);
			if (telefono == null)
				return NotFound();
			return Ok(telefono);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Telefono telefono)
		{
			await _telefonoRepo.AddAsync(telefono);
			await _telefonoRepo.SaveAsync();
			return CreatedAtAction(nameof(GetById), new { num = telefono.Num }, telefono);
		}

		[HttpPut("{num}")]
		public async Task<ActionResult> Update(string num, Telefono telefono)
		{
			if (num != telefono.Num)
				return BadRequest();

			_telefonoRepo.Update(telefono);
			await _telefonoRepo.SaveAsync();
			return NoContent();
		}

		[HttpDelete("{num}")]
		public async Task<ActionResult> Delete(string num)
		{
			var telefono = await _telefonoRepo.GetByIdAsync(num);
			if (telefono == null)
				return NotFound();

			_telefonoRepo.Delete(telefono);
			await _telefonoRepo.SaveAsync();
			return NoContent();
		}
	}
}
