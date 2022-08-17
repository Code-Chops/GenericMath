﻿using CodeChops.DomainDrivenDesign.DomainModeling.Factories;

namespace CodeChops.GenericMath;

/// <summary>
/// From: https://codereview.stackexchange.com/questions/26022/generic-calculator-and-generic-number
/// </summary>
/// <typeparam name="T">Integral type</typeparam>
public readonly record struct Number<T>(T Value) : INumber, IComparable<Number<T>>, IHasEmptyInstance<Number<T>>
	where T : struct, IComparable<T>, IEquatable<T>, IConvertible
{
	public override string? ToString() => $"{this.Value} ({typeof(T).Name})";
	
	public static Number<T> Empty { get; } = new();
	
	public Type GetIntegralType() => typeof(T);

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
		=> new Number<T>(Calculator<T>.Increment(a.Value));

	public static Number<T> operator --(Number<T> a) 
		=> new(Calculator<T>.Decrement(a.Value));

	public static implicit operator Number<T>(T value) 
		=> new(value);

	public static implicit operator T(Number<T> value) 
		=> value.Value;

	public static Number<T> Create<TSourceNumber>(TSourceNumber number)
	{
		if (number is null) throw new ArgumentNullException(nameof(number));

		return new((T)Convert.ChangeType(number, typeof(T)));
	}
}