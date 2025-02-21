using Frasaria.Data.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Frasaria.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
	public DbSet<Frase> Frases{ get; set; }
	public DbSet<FraseFavorita> Favoritas{ get; set; }
}
