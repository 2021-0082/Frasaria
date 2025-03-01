using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Frasaria.Services;

public interface IUsuarioService
{
	Task<string> ObtenerEmail();
	Task<string> ObtenerIdUsuario();
}

public class UsuarioService : IUsuarioService
{
	private readonly AuthenticationStateProvider authProvider;

	public UsuarioService(AuthenticationStateProvider authProvider)
	{
		this.authProvider = authProvider;
	}

	public async Task<string> ObtenerEmail()
	{
		var auth = await authProvider.GetAuthenticationStateAsync();
		return auth.User.FindFirst(ClaimTypes.Email)?.Value ?? "";
	}
	public async Task<string> ObtenerIdUsuario()
	{
		var auth = await authProvider.GetAuthenticationStateAsync();
		return auth.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";

	}

}
