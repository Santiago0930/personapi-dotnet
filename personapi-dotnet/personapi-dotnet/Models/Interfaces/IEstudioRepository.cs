using personapi_dotnet.Interfaces;
using personapi_dotnet.Models.Entities;

namespace personapi_dotnet.Models.Interfaces
{
	public interface IEstudioRepository : IBaseRepository<Estudio>
	{
		Task<Estudio> GetByIdsAsync(int ccPer, int idProf);
	}
}
