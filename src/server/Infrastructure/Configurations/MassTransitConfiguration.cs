using Infrastructure.Configurations.Settings;
using MassTransit;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class MassTransitConfiguration
{
	public static IServiceCollection AddMassTransit(this IServiceCollection services, BrokerSettings settings,
		Action<IBusRegistrationConfigurator>? config = null)
	{
		return services.AddMassTransit(x =>
		{
			config?.Invoke(x);
			
			x.UsingRabbitMq((context, cfg) =>
			{
				cfg.Host(settings.Host, "/", h =>
				{
					h.Username(settings.Username);
					h.Password(settings.Password);
				});

				cfg.ConfigureEndpoints(context);
			});
		});
	}
}