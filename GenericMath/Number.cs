using System.Diagnostics;
using System.Globalization;

namespace CodeChops.GenericMath;

/// <summary>
/// <para>A number of generic type <typeparamref name="T"/>>.</para>
/// <para>See <see cref="Calculator{TNumber}"/> to perform calculations on this number.</para>
/// <para>Adapter from: https://codereview.stackexchange.com/questions/26022/generic-calculator-and-generic-number</para>
/// </summary>
/// <typeparam name="T">Numeric type</typeparam>
[DebuggerDisplay("{Value}{GetSuffix()}")]
public readonly record struct Number<T>(T Value) : INumber<T>
	where T : struct, IComparable<T>, IEquatable<T>, IConvertible
{
	public override string ToString() => this.Value.ToString(CultureInfo.InvariantCulture);
	
	public static Number<T> Zero { get; } = new();
	
	public Type GetNumericType() => typeof(T);

	public int CompareTo(Number<T> other) 
		=> this.Value.CompareTo(other.Value);
	
	public static bool operator <(Number<T> a, Number<T> b) 
		=> a.Value.CompareTo(b.Value) < 0;

	public static bool operator <=(Number<T> a, Number<T> b) 
		=> a.Value.CompareTo(b.Value) <= 0;

	public static bool operator >(Number<T> a, Number<T> b) 
		=> a.Value.CompareTo(b.Value) > 0;

	public static bool operator >=(Number<T> a, Number<T> b) 
		=> a.Value.CompareTo(b.Value) >= 0;

	public static Number<T> operator !(Number<T> a) 
		=> new(Calculator<T>.Negate(a.Value));

	public static Number<T> operator +(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Add(a.Value, b.Value));

	public static Number<T> operator -(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Subtract(a.Value, b.Value));

	public static Number<T> operator *(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Multiply(a.Value, b.Value));

	public static Number<T> operator /(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Divide(a.Value, b.Value));

	public static Number<T> operator %(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Modulo(a.Value, b.Value));

	public static Number<T> operator -(Number<T> a) 
		=> new(Calculator<T>.Negate(a.Value));

	public static Number<T> operator +(Number<T> a) 
		=> new(Calculator<T>.Plus(a.Value));

	public static Number<T> operator ++(Number<T> a) 
		=> new(Calculator<T>.Increment(a.Value));

	public static Number<T> operator --(Number<T> a) 
		=> new(Calculator<T>.Decrement(a.Value));

	public static Number<T> operator <<(Number<T> a, int b) 
		=> new(Calculator<T>.LeftShift(a.Value, b));

	public static Number<T> operator >>(Number<T> a, int b) 
		=> new(Calculator<T>.RightShift(a.Value, b));

	public static Number<T> operator &(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.And(a.Value, b.Value));

	public static Number<T> operator |(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Or(a.Value, b.Value));

	public static Number<T> operator ^(Number<T> a, Number<T> b) 
		=> new(Calculator<T>.Xor(a.Value, b.Value));

	public static Number<T> operator ~(Number<T> a)
		=> new(Calculator<T>.OnesComplement(a.Value));
	
	public static implicit operator Number<T>(T value) 
		=> new(value);

	public static implicit operator T(Number<T> value) 
		=> value.Value;
	
	private string? GetSuffix()
	{
		return this.Value switch
		{
			uint	=> "U",
			long	=> "L",
			ulong	=> "UL",
			float	=> "F",
			double	=> "D",
			decimal => "M",
			_		=> null,
		};
	}
	
	/// <summary>
	/// Converts the primitive of this Number of <typeparamref name="T"/> to primitive of type <typeparamref name="TTarget"/>.
	/// </summary>
	public TTarget ConvertToPrimitive<TTarget>()
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
	{
		return (TTarget)this.Value.ToType(typeof(TTarget), CultureInfo.InvariantCulture);
	}
}