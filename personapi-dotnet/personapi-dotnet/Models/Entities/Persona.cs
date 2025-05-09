using System.ComponentModel.DataAnnotations;

namespace personapi_dotnet.Models.Entities
{
	public partial class Persona
	{
		public Persona()
		{
			Estudios = new HashSet<Estudio>();
			Telefonos = new HashSet<Telefono>();
		}

		public int Cc { get; set; }
		public string Nombre { get; set; } = null!;
		public string Apellido { get; set; } = null!;

		[RegularExpression("^(M|F)$", ErrorMessage = "El género debe ser 'M' o 'F'.")]
		public string? Genero { get; set; }
		public int? Edad { get; set; }

		public virtual ICollection<Estudio> Estudios { get; set; }
		public virtual ICollection<Telefono> Telefonos { get; set; }
	}
}
