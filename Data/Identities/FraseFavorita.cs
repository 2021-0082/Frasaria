using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Frasaria.Data.Identities;

public class FraseFavorita
{
	[Key]
	public int Id { get; set; }

	[Required]
	[StringLength(255)]
	public string UserEmail { get; set; }

	[Required]
	public int FraseId { get; set; }

	[ForeignKey(nameof(FraseId))]
	public virtual Frase Frase { get; set; }
}
