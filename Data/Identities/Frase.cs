using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Frasaria.Data.Identities;

public class Frase
{
	[Key]
	public int Id { get; set; }

	[Required]
	public string Mensaje { get; set; }

	public string Autor { get; set; }

	public string[] Etiquetas { get; set; }
	[Required]
	public string UserId { get; set; }

	[ForeignKey(nameof(UserId))]
	public ApplicationUser User { get; set; }
}
