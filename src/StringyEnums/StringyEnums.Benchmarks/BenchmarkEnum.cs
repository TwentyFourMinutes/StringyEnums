 using System;

namespace StringyEnums.Benchmarks
{
	[Flags]
	[EnumParser.EnumName]
	public enum BenchmarkEnum
	{
		[StringRepresentation("Value 1")]
		[Common.EnumStringValues.EnumStringValue("Value 1")]
		[EnumStringValues.StringValue("Value 1")]
		[EnumParser.EnumName("Value 1")]
		Val1 = 1,
		[StringRepresentation("Value 2")]
		[Common.EnumStringValues.EnumStringValue("Value 2")]
		[EnumStringValues.StringValue("Value 2")]
		[EnumParser.EnumName("Value 2")]
		Val2 = 2,
		[StringRepresentation("Value 3")]
		[Common.EnumStringValues.EnumStringValue("Value 3")]
		[EnumStringValues.StringValue("Value 3")]
		[EnumParser.EnumName("Value 3")]
		Val3 = 4,
		[StringRepresentation("Value 4")]
		[Common.EnumStringValues.EnumStringValue("Value 4")]
		[EnumStringValues.StringValue("Value 4")]
		[EnumParser.EnumName("Value 4")]
		Val4 = 8,
		[StringRepresentation("Value 5")]
		[Common.EnumStringValues.EnumStringValue("Value 5")]
		[EnumStringValues.StringValue("Value 5")]
		[EnumParser.EnumName("Value 5")]
		Val5 = 16,
		[StringRepresentation("Value 6")]
		[Common.EnumStringValues.EnumStringValue("Value 6")]
		[EnumStringValues.StringValue("Value 6")]
		[EnumParser.EnumName("Value 6")]
		Val6 = 32,
		[StringRepresentation("Value 7")]
		[Common.EnumStringValues.EnumStringValue("Value 7")]
		[EnumStringValues.StringValue("Value 7")]
		[EnumParser.EnumName("Value 7")]
		Val7 = 64,
		[StringRepresentation("Value 8")]
		[Common.EnumStringValues.EnumStringValue("Value 8")]
		[EnumStringValues.StringValue("Value 8")]
		[EnumParser.EnumName("Value 8")]
		Val8 = 128,
		[StringRepresentation("Value 9")]
		[Common.EnumStringValues.EnumStringValue("Value 9")]
		[EnumStringValues.StringValue("Value 9")]
		[EnumParser.EnumName("Value 9")]
		Val9 = 256,
		[StringRepresentation("Value 10")]
		[Common.EnumStringValues.EnumStringValue("Value 10")]
		[EnumStringValues.StringValue("Value 10")]
		[EnumParser.EnumName("Value 10")]
		Val10 = 512,

		All = ~(~0 << 10)
	}
}
