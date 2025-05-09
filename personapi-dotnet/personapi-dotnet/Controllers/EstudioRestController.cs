using Microsoft.AspNetCore.Mvc;
using personapi_dotnet.DTOs;
using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EstudioRestController : Controller
	{
		private readonly IEstudioRepository _estudioRepo;

		public EstudioRestController(IEstudioRepository estudioRepo)
		{
			_estudioRepo = estudioRepo;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
		{
			var estudios = await _estudioRepo.GetAllAsync();
			return Ok(estudios);
		}

		[HttpGet("{ccPer}/{idProf}")]
		public async Task<ActionResult<Estudio>> GetById(int ccPer, int idProf)
		{
			var estudio = await _estudioRepo.GetByIdsAsync(ccPer, idProf);
			if (estudio == null)
				return NotFound();
			return Ok(estudio);
		}

		[HttpPost]
		public async Task<ActionResult> Create(EstudioCreateDTO dto)
		{
			var estudio = new Estudio
			{
				CcPer = dto.CcPer,
				IdProf = dto.IdProf,
				Fecha = dto.Fecha,
				Univer = dto.Univer
			};

			await _estudioRepo.AddAsync(estudio);
			await _estudioRepo.SaveAsync();

			return Ok(estudio);
		}


		[HttpPut("{ccPer}/{idProf}")]
		public async Task<ActionResult> Update(int ccPer, int idProf, EstudioCreateDTO dto)
		{
			if (ccPer != dto.CcPer || idProf != dto.IdProf)
				return BadRequest("Los IDs en la ruta y el cuerpo no coinciden.");

			var estudio = await _estudioRepo.GetByIdsAsync(ccPer, idProf);
			if (estudio == null)
				return NotFound();

			estudio.Fecha = dto.Fecha;
			estudio.Univer = dto.Univer;

			_estudioRepo.Update(estudio);
			await _estudioRepo.SaveAsync();

			return NoContent();
		}


		[HttpDelete("{ccPer}/{idProf}")]
		public async Task<ActionResult> Delete(int ccPer, int idProf)
		{
			var estudio = await _estudioRepo.GetByIdsAsync(ccPer, idProf);

			if (estudio == null)
				return NotFound();

			_estudioRepo.Delete(estudio);
			await _estudioRepo.SaveAsync();
			return NoContent();
		}

	}
}
