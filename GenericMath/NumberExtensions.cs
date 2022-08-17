namespace CodeChops.GenericMath;

public static class NumberExtensions
{
	public static Number<TTarget> Cast<TSource, TTarget>(this Number<TSource> number)
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
		where TSource : struct, IComparable<TSource>, IEquatable<TSource>, IConvertible
	{
		return new((TTarget)Convert.ChangeType((TSource)number, typeof(TTarget)));
	}
	
	public static TTarget CastToPrimitive<TSource, TTarget>(this Number<TSource> number)
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
		where TSource : struct, IComparable<TSource>, IEquatable<TSource>, IConvertible
	{
		return (TTarget)Convert.ChangeType((TSource)number, typeof(TTarget));
	}
}