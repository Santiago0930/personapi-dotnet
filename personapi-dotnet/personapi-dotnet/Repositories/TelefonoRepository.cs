using personapi_dotnet.Models.Entities;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Interfaces;

namespace personapi_dotnet.Repositories
{
	public class TelefonoRepository : ITelefonoRepository
	{
		private readonly persona_dbContext _context;

		public TelefonoRepository(persona_dbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Telefono>> GetAllAsync()
		{
			return await _context.Telefonos
				.Include(t => t.DuenioNavigation)
				.ToListAsync();
		}

		public async Task<Telefono> GetByIdAsync(object id)
		{
			var num = id as string;
			if (num == null) return null;

			return await _context.Telefonos
				.Include(t => t.DuenioNavigation)
				.FirstOrDefaultAsync(t => t.Num == num);
		}

		public async Task AddAsync(Telefono entity)
		{
			await _context.Telefonos.AddAsync(entity);
		}

		public void Update(Telefono entity)
		{
			_context.Telefonos.Update(entity);
		}

		public void Delete(Telefono entity)
		{
			_context.Telefonos.Remove(entity);
		}

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
