using personapi_dotnet.Models.Entities;
using personapi_dotnet.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Repositories
{
	public class PersonaRepository : IPersonaRepository
	{
		private readonly persona_dbContext _context;

		public PersonaRepository(persona_dbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Persona>> GetAllAsync()
		{
			return await _context.Personas.ToListAsync();
		}

		public async Task<Persona> GetByIdAsync(object id)
		{
			return await _context.Personas.FindAsync(id);
		}

		public async Task AddAsync(Persona entity)
		{
			await _context.Personas.AddAsync(entity);
		}

		public void Update(Persona entity)
		{
			_context.Personas.Update(entity);
		}

		public void Delete(Persona entity)
		{
			_context.Personas.Remove(entity);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
