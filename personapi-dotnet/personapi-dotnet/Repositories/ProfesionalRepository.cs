using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Repositories
{
	public class ProfesionRepository : IProfesionRepository
	{
		private readonly persona_dbContext _context;

		public ProfesionRepository(persona_dbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Profesion>> GetAllAsync()
		{
			return await _context.Profesions.ToListAsync();
		}

		public async Task<Profesion> GetByIdAsync(object id)
		{
			return await _context.Profesions.FindAsync(id);
		}

		public async Task AddAsync(Profesion entity)
		{
			await _context.Profesions.AddAsync(entity);
		}

		public void Update(Profesion entity)
		{
			_context.Profesions.Update(entity);
		}

		public void Delete(Profesion entity)
		{
			_context.Profesions.Remove(entity);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
