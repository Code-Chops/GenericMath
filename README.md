# Generic math

Provides a way to perform generic math functions on the following numeric types: `byte`, `sbyte`, `decimal`, `double`, `float`, `int`, `uint`, `nint`, `nuint`, `long`, `ulong`, `short`, `ushort`. The following operations are supported: add, subtract, multiply, divide, modulo, negate, plus, increment, decrement, leftShift, rightShift, onesComplement, and, or, xor.

Other features:
- Supports converting at runtime (when the type to convert to/from is not known at compile time). 
- Contains a JSON converter which cleanly and correctly converts these numbers from and to JSON.


### Note:
- This functionality can nowadays also be achieved by using the GenericMath functionality of .NET 7. C# 11 also has to be used because it makes use of static abstract members in interfaces the achieve its goal, which is introduced in C# 11. See: [Microsoft - Generic math](https://learn.microsoft.com/en-us/dotnet/standard/generics/math)
- This code is adapted from: [StackExchange - Generic Calculator and Generic Number](https://codereview.stackexchange.com/questions/26022/generic-calculator-and-generic-number)
