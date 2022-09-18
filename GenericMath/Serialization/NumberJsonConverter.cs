using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeChops.GenericMath.Serialization;

public class NumberJsonConverter<TNumber> : JsonConverter<Number<TNumber>>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	private static TypeCode ExpectedTypeCode { get; } = Type.GetTypeCode(typeof(TNumber));
	private static JsonConverter<TNumber> DefaultConverter { get; } = (JsonConverter<TNumber>)new JsonSerializerOptions().GetConverter(typeof(TNumber));
	
	public override Number<TNumber> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Check for null values
		if (reader.TokenType == JsonTokenType.Null) return default;

		if (reader.TokenType != JsonTokenType.Number) throw new JsonException($"Unexpected token found in JSON: {reader.TokenType}. Expected: {JsonTokenType.Number}.");

		var typeCode = Type.GetTypeCode(typeToConvert);
		if (typeCode != TypeCode.Object && typeCode != ExpectedTypeCode) throw new JsonException($"Can't convert {typeCode} to {ExpectedTypeCode}.");

		var number = DefaultConverter.Read(ref reader, typeof(TNumber), options);
		return number;
	}

	public override void Write(Utf8JsonWriter writer, Number<TNumber> number, JsonSerializerOptions options)
		=> DefaultConverter.Write(writer, number.Value, options);
}