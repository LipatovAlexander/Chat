using Infrastructure;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class DbContextConfiguration
{
	public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		return services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connectionString); });
	}
}