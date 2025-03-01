using Frasaria.Data.Identities;
using Frasaria.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Frasaria.Data.Request;
using Frasaria.Data.Response;

namespace Frasaria.Services;

public interface IFrasesService
{
	Task<(bool Success, string Message)> ActualizarFrase(int fraseId, FraseRequest frase);
	Task<(bool Success, string Message)> Eliminar(int id);
	Task<(bool Success, string Message)> GuardarFrase(FraseRequest frase);
	Task<FraseResponse?> ObtenerPorId(int id);
	Task<List<FraseResponse>> ObtenerTodas(string filter = "");
}

public class FrasesService : IFrasesService
{
	private readonly ApplicationDbContext _context;
	private readonly IUsuarioService usuarioService;

	public FrasesService(ApplicationDbContext context, AuthenticationStateProvider authProvider, IUsuarioService usuarioService)
	{
		_context = context;
		this.usuarioService = usuarioService;
	}

	public async Task<List<FraseResponse>> ObtenerTodas(string filter = "")
	{
		filter = filter.ToLower();
		var frases = await _context.Frases.Where(x =>
					x.Mensaje.ToLower().Contains(filter) ||
					x.Etiquetas.Any(x => x.ToLower() == filter) ||
					x.Autor.ToLower().Contains(filter)
				).Select(x => x.ToRespuesta())
				.ToListAsync();
		return frases;
	}

	public async Task<FraseResponse?> ObtenerPorId(int id)
	{
		var result = await _context.Frases.FindAsync(id);
		return result?.ToRespuesta();

	}

	public async Task<(bool Success, string Message)> GuardarFrase(FraseRequest frase)
	{
		if (string.IsNullOrWhiteSpace(frase.Mensaje))
			return (false, "El mensaje no puede estar vacío.");

		if (frase.Mensaje.Length < 5)
			return (false, "El mensaje debe tener al menos 5 caracteres.");

		if (string.IsNullOrWhiteSpace(frase.Autor))
			return (false, "El autor es obligatorio.");
		var agregar = new Frase(frase);
		agregar.UserId = await usuarioService.ObtenerIdUsuario();

		_context.Frases.Add(agregar);
		await _context.SaveChangesAsync();
		return (true, "Frase agregada exitosamente.");
	}

	public async Task<(bool Success, string Message)> ActualizarFrase(int fraseId, FraseRequest frase)
	{
		var fraseActualizar = await _context.Frases.FirstOrDefaultAsync(x => x.Id == fraseId);
		if (fraseActualizar == null)
		{
			return (false, "No se encontro dicha frase");
		}
		if (fraseActualizar.Actualizar(frase.Mensaje, frase.Autor, frase.Etiquetas))
		{
			await _context.SaveChangesAsync();
			return (true, "Frase actualizada");
		}
		return (false, "Sin cambios realizados");
	}

	public async Task<(bool Success, string Message)> Eliminar(int id)
	{
		var frase = await _context.Frases.FindAsync(id);
		if (frase == null)
		{
			return (false, "Frase no encontrada");
		}
		var GuardadaFavorita = await _context.Favoritas.AnyAsync(f => f.FraseId == id);
		if (GuardadaFavorita)
		{
			return (false, "No se puede eliminar esta frase ya que esta marcada como favorita para alguien");
		}
		_context.Frases.Remove(frase);
		await _context.SaveChangesAsync();
		return (true, "Frase Eliminada");
	}
}
