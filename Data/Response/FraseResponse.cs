using System.ComponentModel.DataAnnotations;

namespace Frasaria.Data.Response;

public class FraseResponse
{
	public int id { get; set; }

	public string Mensaje { get; set; }

	public string Autor { get; set; }

	public string[] Etiquetas { get; set; }

	public string UserId { get; set; }

}
