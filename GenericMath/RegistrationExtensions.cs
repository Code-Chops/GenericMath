using CodeChops.GenericMath.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;

namespace CodeChops.GenericMath;

public static class RegistrationExtensions
{
	public static IServiceCollection AddGenericNumberJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());

		return services;
	}
}