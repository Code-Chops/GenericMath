namespace CodeChops.GenericMath;

/// <summary>
/// Class to allow operations (like Add, Multiply, etc.) for generic types. This type should allow these operations themselves.
/// From: https://codereview.stackexchange.com/questions/26022/generic-calculator-and-generic-number
/// </summary>
public static partial class Calculator<T>
{
	static Calculator()
	{
		Add				= CreateDelegate<T>(Expression.AddChecked,		nameof(Add),			isChecked: true);
		Subtract		= CreateDelegate<T>(Expression.SubtractChecked, nameof(Subtract),		isChecked: true);
		Multiply		= CreateDelegate<T>(Expression.MultiplyChecked, nameof(Multiply),		isChecked: true);
		Divide			= CreateDelegate<T>(Expression.Divide, 			nameof(Divide),			isChecked: true);
		Modulo			= CreateDelegate<T>(Expression.Modulo, 			nameof(Modulo),			isChecked: true);
		Negate			= CreateDelegate(Expression.NegateChecked,		nameof(Negate),			isChecked: true);
		Plus			= CreateDelegate(Expression.UnaryPlus, 			nameof(Plus),			isChecked: true);
		Increment		= CreateDelegate(Expression.Increment, 			nameof(Increment), 		isChecked: true);
		Decrement		= CreateDelegate(Expression.Decrement, 			nameof(Decrement), 		isChecked: true);
		LeftShift		= CreateDelegate<int>(Expression.LeftShift,		nameof(LeftShift),		isChecked: false);
		RightShift		= CreateDelegate<int>(Expression.RightShift,	nameof(RightShift), 	isChecked: false);
		OnesComplement	= CreateDelegate(Expression.OnesComplement,		nameof(OnesComplement),	isChecked: false);
		And				= CreateDelegate<T>(Expression.And,				nameof(And),			isChecked: false);
		Or				= CreateDelegate<T>(Expression.Or,				nameof(Or),				isChecked: false);
		Xor				= CreateDelegate<T>(Expression.ExclusiveOr,		nameof(Xor),			isChecked: false);
	}

	/// <summary>
	/// Adds two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T, T> Add { get; }

	/// <summary>
	/// Subtracts two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T, T> Subtract { get; }

	/// <summary>
	/// Multiplies two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T, T> Multiply { get; }

	/// <summary>
	/// Divides two values of the same type.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T, T> Divide { get; }

	/// <summary>
	/// Divides two values of the same type and returns the remainder.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T, T> Modulo { get; }

	/// <summary>
	/// Gets the negative value of T.
	/// Supported by: All numeric values, but will throw an OverflowException on unsigned values which are not 0.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T> Negate { get; }

	/// <summary>
	/// Gets the plus value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T> Plus { get; }

	/// <summary>
	/// Gets the incremental value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T> Increment { get; }

	/// <summary>
	/// Gets the decremental value of T.
	/// Supported by: All numeric values.
	/// </summary>
	/// <exception cref="OverflowException"/>
	/// <exception cref="InvalidOperationException"/>
	public static Func<T, T> Decrement { get; }

	/// <summary>
	/// Shifts the number to the left.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, int, T> LeftShift;

	/// <summary>
	/// Shifts the number to the right.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, int, T> RightShift;

	/// <summary>
	/// Inverts all bits inside the value.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, T> OnesComplement;

	/// <summary>
	/// Performs a bitwise OR.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, T, T> Or;

	/// <summary>
	/// Performs a bitwise AND
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, T, T> And;

	/// <summary>
	/// Performs a bitwise Exclusive OR.
	/// Supported by: All integral types.
	/// </summary>
	/// <exception cref="InvalidOperationException"/>
	public static readonly Func<T, T, T> Xor;

	private static Func<T, T2, T> CreateDelegate<T2>(Func<Expression, Expression, Expression> @operator, string operatorName, bool isChecked)
	{
		try
		{
			var convertToTypeA = ConvertTo(typeof(T));
			var convertToTypeB = ConvertTo(typeof(T2));
			var parameterA = Expression.Parameter(typeof(T), "a");
			var parameterB = Expression.Parameter(typeof(T2), "b");
			var valueA = convertToTypeA != null ? Expression.Convert(parameterA, convertToTypeA) : (Expression)parameterA;
			var valueB = convertToTypeB != null ? Expression.Convert(parameterB, convertToTypeB) : (Expression)parameterB;
			var body = @operator(valueA, valueB);

			if (convertToTypeA != null) 
				body = isChecked ? Expression.ConvertChecked(body, typeof(T)) : Expression.Convert(body, typeof(T));

			return Expression.Lambda<Func<T, T2, T>>(body, parameterA, parameterB).Compile();
		}
		catch
		{
			return (_, _) => throw new InvalidOperationException($"Operator {operatorName} is not supported by type {typeof(T).FullName}.");
		}
	}

	private static Func<T, T> CreateDelegate(Func<Expression, Expression> @operator, string operatorName, bool isChecked)
	{
		try
		{
			var convertToType = ConvertTo(typeof(T));
			var parameter = Expression.Parameter(typeof(T), "a");
			var value = convertToType != null ? Expression.Convert(parameter, convertToType) : (Expression)parameter;
			var body = @operator(value);

			if (convertToType != null)
			{
				body = isChecked ? Expression.ConvertChecked(body, typeof(T)) : Expression.Convert(body, typeof(T));
			}

			return Expression.Lambda<Func<T, T>>(body, parameter).Compile();
		}
		catch
		{
			return _ => throw new InvalidOperationException($"Operator {operatorName} is not supported by type {typeof(T).FullName}.");
		}
	}

	private static Type? ConvertTo(Type type)
	{
		return Type.GetTypeCode(type) is TypeCode.Char or TypeCode.Byte or TypeCode.SByte or TypeCode.Int16 or TypeCode.UInt16
			? typeof(int)
			: null;
	}
}