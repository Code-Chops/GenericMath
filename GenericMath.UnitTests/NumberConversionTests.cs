using System.Text.Json;
using CodeChops.GenericMath.Serialization.Json;
using Xunit;

namespace CodeChops.GenericMath.UnitTests;

public class NumberConversionTests
{
	private static Number<int> NumberInt { get; } = new(7);
	private const string NumberIntJson = "7";
	private static Number<double> NumberDouble { get; } = new(3.12);
	private const string NumberDoubleJson = "3.12";

	private static NumberWrapperContractMock NumberWrapperContract { get; } = new() { IntNumber = NumberInt, DoubleNumber = NumberDouble };
	private const string NumberWrapperJson = @$"{{""{nameof(NumberWrapperContractMock.IntNumber)}"":7,""{nameof(NumberWrapperContractMock.DoubleNumber)}"":3.12}}";

	static NumberConversionTests()
	{
		JsonSerialization.DefaultOptions.Converters.Add(new NumberJsonConverterFactory());
	}
	
	[Fact]
	public void Deserialization_NumberInt_Is_Correct()
	{
		var number = JsonSerializer.Deserialize<Number<int>>(NumberIntJson, JsonSerialization.DefaultOptions);

		Assert.Equal(typeof(Number<int>), number.GetType());
		Assert.Equal(NumberInt.Value, number.Value);
	}

	[Fact]
	public void Serialization_NumberInt_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberInt, JsonSerialization.DefaultOptions);
        
		Assert.Equal(NumberIntJson, json);
	}
	
	[Fact]
	public void Deserialization_NumberDouble_Is_Correct()
	{
		var number = JsonSerializer.Deserialize<Number<double>>(NumberDoubleJson, JsonSerialization.DefaultOptions);

		Assert.Equal(typeof(Number<double>), number.GetType());
		Assert.Equal(NumberDouble.Value, number.Value);
	}

	[Fact]
	public void Serialization_NumberDouble_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberDouble, JsonSerialization.DefaultOptions);
        
		Assert.Equal(NumberDoubleJson, json);
	}
	
	[Fact]
	public void Deserialization_NumberWrapper_Is_Correct()
	{
		var numberWrapper = JsonSerializer.Deserialize<NumberWrapperContractMock>(NumberWrapperJson, JsonSerialization.DefaultOptions)!;
		Assert.NotNull(numberWrapper);
		
		Assert.Equal(typeof(NumberWrapperContractMock), numberWrapper.GetType());
		Assert.Equal(NumberDouble.Value, numberWrapper.DoubleNumber.Value);
		Assert.Equal(NumberInt.Value, numberWrapper.IntNumber.Value);	
	}
	
	[Fact]
	public void Serialization_NumberWrapper_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberWrapperContract, JsonSerialization.DefaultOptions);
        
		Assert.Equal(NumberWrapperJson, json);
	}
}