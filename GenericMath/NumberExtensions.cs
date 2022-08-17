namespace CodeChops.GenericMath;

public static class NumberExtensions
{
	public static Number<TNumber> Cast<TSourceNumber, TNumber>(this Number<TSourceNumber> number)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
		where TSourceNumber : struct, IComparable<TSourceNumber>, IEquatable<TSourceNumber>, IConvertible
	{
		return new((TNumber)Convert.ChangeType((TSourceNumber)number, typeof(TNumber)));
	}
	
	public static TNumber CastToPrimitive<TSourceNumber, TNumber>(this Number<TSourceNumber> number)
		where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
		where TSourceNumber : struct, IComparable<TSourceNumber>, IEquatable<TSourceNumber>, IConvertible
	{
		return (TNumber)Convert.ChangeType((TSourceNumber)number, typeof(TNumber));
	}
}