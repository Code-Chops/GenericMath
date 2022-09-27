using CodeChops.GenericMath.Serialization.Json;
using Microsoft.Extensions.DependencyInjection;
using CodeChops.DomainDrivenDesign.DomainModeling.Serialization;

namespace CodeChops.GenericMath;

public static class RegistrationExtensions
{
	public static IServiceCollection AddGenericNumberJsonSerialization(this IServiceCollection services)
	{
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());

		return services;
	}
}