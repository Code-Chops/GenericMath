// using System.Text.Json;
// using System.Text.Json.Serialization;
//
// namespace CodeChops.GenericMath;
//
// public class NumberConverter<T> : JsonConverter<Number<T>>
// 	where T : struct, IComparable<T>, IEquatable<T>, IConvertible
// {
// 	public override bool CanConvert(Type objectType)
// 	{
// 		return objectType == typeof(Number<>);
// 	}
//
// 	public override Number<T> Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
// 	{
// 		return typeof(T) switch
// 		{
// 			JsonTokenType. => new Number<int>(Int32.Parse(input.ToString())),
// 			JsonTokenType.Float => new Number<float>(Single.Parse(input.ToString())),
// 			JsonTokenType.Bytes => new Number<byte>(Byte.Parse(input.ToString())),
// 			_ => throw new ArgumentOutOfRangeException(),
// 		};
// 	}
//
// 	public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
// 	{
// 		var numberAsText = value switch
// 		{
// 			Number<Decimal> or Number<Double> or Number<Single> => value.ToString(),
// 			Number<Int16> or Number<Int32> or Number<Int64> => ((int)(Number<int>)value).ToString(),
// 			_ => throw new ArgumentOutOfRangeException(),
// 		};
//
// 		writer.WriteRawValue(numberAsText);
// 	}
// }