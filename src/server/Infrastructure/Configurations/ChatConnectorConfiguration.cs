using Infrastructure;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ChatConnectorConfiguration
{
	public static IServiceCollection AddChatConnector<THub>(this IServiceCollection services) where THub : Hub
	{
		return services.AddScoped<IChatConnector, ChatConnector>(provider =>
		{
			var dbContext = provider.GetRequiredService<ApplicationDbContext>();
			var hubContext = provider.GetRequiredService<IHubContext<THub>>();
			return new ChatConnector(dbContext, (IHubContext) hubContext);
		});
	}
}