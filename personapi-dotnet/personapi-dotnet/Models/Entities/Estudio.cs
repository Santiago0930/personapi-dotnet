using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace personapi_dotnet.Models.Entities
{
	public partial class Estudio
	{
		public int IdProf { get; set; }
		public int CcPer { get; set; }
		public DateTime? Fecha { get; set; }
		public string? Univer { get; set; }

		[ValidateNever]
		public virtual Persona CcPerNavigation { get; set; } = null!;
		[ValidateNever]
		public virtual Profesion IdProfNavigation { get; set; } = null!;
	}
}