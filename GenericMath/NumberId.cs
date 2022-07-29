using CodeChops.GenericMath;

// ReSharper disable once CheckNamespace
namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Uses generic number with <typeparamref name="TNumber"/> as the type of the ID.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
/// <typeparam name="TNumber"></typeparam>
public abstract record NumberId<TSelf, TNumber> : Id<TSelf, Number<TNumber>>
	where TSelf : NumberId<TSelf, TNumber>
	where TNumber : struct, IComparable<TNumber>, IEquatable<TNumber>, IConvertible
{
	protected NumberId(TNumber number)
		: base(new Number<TNumber>(number))
	{
	}
}