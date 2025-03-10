﻿using System.ComponentModel.DataAnnotations;

namespace Frasaria.Data.Request;

public class FraseRequest
{
	public int Id { get; set; }
	[Required]
	public string Mensaje { get; set; }

	[Required]
	public string Autor { get; set; }

	[Required]
	public string[] Etiquetas { get; set; }
}
