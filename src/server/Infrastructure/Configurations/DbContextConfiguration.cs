using Infrastructure;
using Infrastructure.Configurations.Settings;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DbContextConfiguration
{
	public static IServiceCollection AddApplicationDbContext(this IServiceCollection services,
		ConnectionStringsSettings settings)
	{
		return services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(settings.Postgres); });
	}
}