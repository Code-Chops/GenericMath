using CodeChops.GenericMath;

// ReSharper disable once CheckNamespace
namespace CodeChops.DomainDrivenDesign.DomainModeling.Identities;

/// <summary>
/// Uses a generic number as ID. With <typeparamref name="TPrimitive"/> as the primitive type of the ID.
/// </summary>
public abstract record NumberId<TSelf, TPrimitive> : Id<TSelf, Number<TPrimitive>>
	where TSelf : NumberId<TSelf, TPrimitive>
	where TPrimitive : struct, IComparable<TPrimitive>, IEquatable<TPrimitive>, IConvertible
{
	protected NumberId(TPrimitive number)
		: base(new Number<TPrimitive>(number))
	{
	}
}