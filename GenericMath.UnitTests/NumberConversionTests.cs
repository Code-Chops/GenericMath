using System.Text.Json;
using CodeChops.GenericMath.Serialization;
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
	private JsonSerializerOptions JsonSerializerOptions { get; }

	public NumberConversionTests()
	{
		this.JsonSerializerOptions = new()
		{
			WriteIndented = false, 
			Converters = { new NumberJsonConverterFactory() }
		};
	}
	
	[Fact]
	public void Deserialization_NumberInt_Is_Correct()
	{
		var number = JsonSerializer.Deserialize<Number<int>>(NumberIntJson, this.JsonSerializerOptions);

		Assert.Equal(typeof(Number<int>), number.GetType());
		Assert.Equal(NumberInt.Value, number.Value);
	}

	[Fact]
	public void Serialization_NumberInt_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberInt, this.JsonSerializerOptions);
        
		Assert.Equal(NumberIntJson, json);
	}
	
	[Fact]
	public void Deserialization_NumberDouble_Is_Correct()
	{
		var number = JsonSerializer.Deserialize<Number<double>>(NumberDoubleJson, this.JsonSerializerOptions);

		Assert.Equal(typeof(Number<double>), number.GetType());
		Assert.Equal(NumberDouble.Value, number.Value);
	}

	[Fact]
	public void Serialization_NumberDouble_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberDouble, this.JsonSerializerOptions);
        
		Assert.Equal(NumberDoubleJson, json);
	}
	
	[Fact]
	public void Deserialization_NumberWrapper_Is_Correct()
	{
		var numberWrapper = JsonSerializer.Deserialize<NumberWrapperContractMock>(NumberWrapperJson, this.JsonSerializerOptions)!;
		Assert.NotNull(numberWrapper);
		
		Assert.Equal(typeof(NumberWrapperContractMock), numberWrapper.GetType());
		Assert.Equal(NumberDouble.Value, numberWrapper.DoubleNumber.Value);
		Assert.Equal(NumberInt.Value, numberWrapper.IntNumber.Value);	
	}
	
	[Fact]
	public void Serialization_NumberWrapper_Is_Correct()
	{
		var json = JsonSerializer.Serialize(NumberWrapperContract, this.JsonSerializerOptions);
        
		Assert.Equal(NumberWrapperJson, json);
	}
}