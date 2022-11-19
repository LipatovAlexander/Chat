using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configurations;

public static class DbContextConfiguration
{
	public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, string connectionString)
	{
		return services.AddDbContext<ApplicationDbContext>(options => { options.UseNpgsql(connectionString); });
	}
}