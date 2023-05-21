using Envelope.Database.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Envelope.Database;

public static partial class ServiceCollectionExtensions
{
	public static IServiceCollection AddDatabaseModelFactory(this IServiceCollection services)
	{
		services.TryAddSingleton<IDatabaseModelFactory, DatabaseModelFactory>();
		return services;
	}
}
