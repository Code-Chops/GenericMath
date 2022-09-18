using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.GenericMath.Serialization;

public class NumberJsonConverterFactory : JsonConverterFactory
{
	public override bool CanConvert(Type typeToConvert) 
		=> !typeToConvert.IsAbstract && typeToConvert.IsAssignableTo(typeof(INumber));

	public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
	{
		var numberType = typeToConvert.GetGenericArguments()[0];
		var converter = (JsonConverter)Activator.CreateInstance(
			type: typeof(NumberJsonConverter<>).MakeGenericType(numberType),
			bindingAttr: BindingFlags.Instance | BindingFlags.Public,
			binder: null,
			args: null,
			culture: null)!;

		return converter;
	}
}