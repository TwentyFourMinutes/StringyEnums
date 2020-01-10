using System;

namespace StringyEnums.Shared.Models
{
	[Flags]
	public enum TestByteEnum : byte
	{
		[StringRepresentation("Enum 1")]
		EnumOne = 1,
		[StringRepresentation("Enum 2")]
		EnumTwo = 2,
		[StringRepresentation("Enum 3")]
		EnumThree = 4,
		[StringRepresentation("Enum 4")]
		EnumFour = 8
	}
}
