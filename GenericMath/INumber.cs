using CodeChops.DomainDrivenDesign.DomainModeling;

namespace CodeChops.GenericMath;

public interface INumber : IValueObject
{
	Type GetIntegralType();
}