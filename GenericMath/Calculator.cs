namespace CodeChops.GenericMath;

/// <summary>
/// <para>Performs calculative operations (like add, multiply, etc.) on <see cref="Number{T}"/>.</para>
/// <para>Adapted from: https://codereview.stackexchange.com/questions/26022/generic-calculator-and-generic-number</para>
/// </summary>
public static class Calculator<TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	static Calculator()
	{
		Add				= CreateDelegate<TNumber>(Expression.AddChecked,		nameof(Add),			isChecked: true);
		Subtract		= CreateDelegate<TNumber>(Expression.SubtractChecked,	nameof(Subtract),		isChecked: true);
		Multiply		= CreateDelegate<TNumber>(Expression.MultiplyChecked,	nameof(Multiply),		isChecked: true);
		Divide			= CreateDelegate<TNumber>(Expression.Divide, 			nameof(Divide),			isChecked: true);
		Modulo			= CreateDelegate<TNumber>(Expression.Modulo, 			nameof(Modulo),			isChecked: true);
		Negate			= CreateDelegate(Expression.NegateChecked,				nameof(Negate),			isChecked: true);
		Plus			= CreateDelegate(Expression.UnaryPlus, 					nameof(Plus),			isChecked: true);
		Increment		= CreateDelegate(Expression.Increment, 					nameof(Increment), 		isChecked: true);
		Decrement		= CreateDelegate(Expression.Decrement, 					nameof(Decrement), 		isChecked: true);
		LeftShift		= CreateDelegate<int>(Expression.LeftShift,				nameof(LeftShift),		isChecked: false);
		RightShift		= CreateDelegate<int>(Expression.RightShift,			nameof(RightShift), 	isChecked: false);
		OnesComplement	= CreateDelegate(Expression.OnesComplement,				nameof(OnesComplement),	isChecked: false);
		And				= CreateDelegate<TNumber>(Expression.And,				nameof(And),			isChecked: false);
		Or				= CreateDelegate<TNumber>(Expression.Or,				nameof(Or),				isChecked: false);
		Xor				= CreateDelegate<TNumber>(Expression.ExclusiveOr,		nameof(Xor),			isChecked: false);
	}

	/// <summary>
	/// Adds two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber, TNumber> Add { get; }

	/// <summary>
	/// Subtracts two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber, TNumber> Subtract { get; }

	/// <summary>
	/// Multiplies two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber, TNumber> Multiply { get; }

	/// <summary>
	/// Divides two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber, TNumber> Divide { get; }

	/// <summary>
	/// Divides two values of the same type and returns the remainder.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber, TNumber> Modulo { get; }

	/// <summary>
	/// Gets the negative value of T.
	/// Supported by: All numeric values, but will throw an OverflowException on unsigned values which are not 0.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber> Negate { get; }

	/// <summary>
	/// Gets the plus value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber> Plus { get; }

	/// <summary>
	/// Gets the incremental value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber> Increment { get; }

	/// <summary>
	/// Gets the decremental value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<TNumber, TNumber> Decrement { get; }

	/// <summary>
	/// Shifts the number to the left.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, int, TNumber> LeftShift;

	/// <summary>
	/// Shifts the number to the right.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, int, TNumber> RightShift;

	/// <summary>
	/// Inverts all bits inside the value.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, TNumber> OnesComplement;

	/// <summary>
	/// Performs a bitwise OR.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, TNumber, TNumber> Or;

	/// <summary>
	/// Performs a bitwise AND
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, TNumber, TNumber> And;

	/// <summary>
	/// Performs a bitwise Exclusive OR.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<TNumber, TNumber, TNumber> Xor;

	private static Func<TNumber, T2, TNumber> CreateDelegate<T2>(Func<Expression, Expression, Expression> @operator, string operatorName, bool isChecked)
	{
		try
		{
			var convertToTypeA = ConvertTo(typeof(TNumber));
			var convertToTypeB = ConvertTo(typeof(T2));
			var parameterA = Expression.Parameter(typeof(TNumber), "a");
			var parameterB = Expression.Parameter(typeof(T2), "b");
			var valueA = convertToTypeA != null ? Expression.Convert(parameterA, convertToTypeA) : (Expression)parameterA;
			var valueB = convertToTypeB != null ? Expression.Convert(parameterB, convertToTypeB) : (Expression)parameterB;
			var body = @operator(valueA, valueB);

			if (convertToTypeA != null) 
				body = isChecked ? Expression.ConvertChecked(body, typeof(TNumber)) : Expression.Convert(body, typeof(TNumber));

			return Expression.Lambda<Func<TNumber, T2, TNumber>>(body, parameterA, parameterB).Compile();
		}
		catch
		{
			return (_, _) => throw new InvalidOperationException($"Operator {operatorName} is not supported by type {typeof(TNumber).FullName}.");
		}
	}

	private static Func<TNumber, TNumber> CreateDelegate(Func<Expression, Expression> @operator, string operatorName, bool isChecked)
	{
		try
		{
			var convertToType = ConvertTo(typeof(TNumber));
			var parameter = Expression.Parameter(typeof(TNumber), "a");
			var value = convertToType != null ? Expression.Convert(parameter, convertToType) : (Expression)parameter;
			var body = @operator(value);

			if (convertToType != null)
			{
				body = isChecked ? Expression.ConvertChecked(body, typeof(TNumber)) : Expression.Convert(body, typeof(TNumber));
			}

			return Expression.Lambda<Func<TNumber, TNumber>>(body, parameter).Compile();
		}
		catch
		{
			return _ => throw new InvalidOperationException($"Operator {operatorName} is not supported by type {typeof(TNumber).FullName}.");
		}
	}

	private static Type? ConvertTo(Type type)
	{
		return Type.GetTypeCode(type) is TypeCode.Char or TypeCode.Byte or TypeCode.SByte or TypeCode.Int16 or TypeCode.UInt16
			? typeof(int)
			: null;
	}
	
	/// <summary>
	/// Converts a primitive of type <typeparamref name="TSource"/> to type <typeparamref name="TTarget"/>.
	/// <para><b>Is slow and performs boxing!</b></para>
	/// </summary>
	public static TTarget ConvertPrimitive<TSource, TTarget>(TSource value)
		where TSource : struct, IComparable<TSource>, IEquatable<TSource>, IConvertible
		where TTarget : struct, IComparable<TTarget>, IEquatable<TTarget>, IConvertible
	{
		return (TTarget)Convert.ChangeType(value, typeof(TTarget));
	}
}