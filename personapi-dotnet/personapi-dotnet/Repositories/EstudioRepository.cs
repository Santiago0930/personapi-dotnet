using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Repositories
{
	public class EstudioRepository : IEstudioRepository
	{
		private readonly persona_dbContext _context;

		public EstudioRepository(persona_dbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Estudio>> GetAllAsync()
		{
			return await _context.Estudios
		.Include(e => e.CcPerNavigation)
		.Include(e => e.IdProfNavigation)
		.ToListAsync();
		}

		public async Task<Estudio> GetByIdAsync(object id)
		{
			return await _context.Estudios.FindAsync(id);
		}

		public async Task AddAsync(Estudio entity)
		{
			await _context.Estudios.AddAsync(entity);
		}

		public void Update(Estudio entity)
		{
			_context.Estudios.Update(entity);
		}

		public void Delete(Estudio entity)
		{
			_context.Estudios.Remove(entity);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<Estudio> GetByIdsAsync(int ccPer, int idProf)
		{
			return await _context.Estudios.FindAsync(idProf, ccPer);
		}

	}
}
