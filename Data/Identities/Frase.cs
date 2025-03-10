﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Frasaria.Data.Response;
using Frasaria.Data.Request;

namespace Frasaria.Data.Identities;

public class Frase
{
	private FraseRequest frase;

	public Frase(FraseRequest frase)
	{
		this.frase = frase;
	}
	public Frase()
	{
		
	}
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

	public bool Actualizar(string mensaje, string autor, string[] etiquetas)
	{
		var cambios = false;
		if (this.Mensaje != mensaje)
		{
			this.Mensaje = mensaje;
			cambios = true;
		}
		if (this.Autor != autor)
		{
			this.Autor = autor;
			cambios = true;
		}
		if (this.Etiquetas != Etiquetas)
		{
			this.Etiquetas = Etiquetas;
			cambios = true;
		}
		return cambios;
	}

	public FraseResponse ToRespuesta()
	{
		return new FraseResponse()
		{
			id = this.Id,
			Mensaje = this.Mensaje,
			Autor = this.Autor,
			Etiquetas = this.Etiquetas,
			UserId = this.UserId
		};

	}
}
