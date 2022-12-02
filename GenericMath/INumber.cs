namespace CodeChops.GenericMath;

public interface INumber<T> : INumber, IComparable<Number<T>> 
	where T : struct, IComparable<T>, IEquatable<T>, IConvertible
{
}

public interface INumber
{
	Type GetNumericType();
}