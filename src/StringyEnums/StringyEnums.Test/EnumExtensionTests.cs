using BidirectionalDict;
using StringyEnums.Shared.Models;
using Xunit;

namespace StringyEnums.Test
{
	public class EnumExtensionTests
	{
		public EnumExtensionTests()
		{
			EnumCore.Init(init =>
			{
				init.InitWith<TestEnum>();
				init.InitWith<TestByteEnum>();
				init.InitWith<TestUShortEnum>();
			});
		}

		[Theory]
		[InlineData(TestEnum.EnumOne, "Enum 1")]
		[InlineData(TestEnum.EnumTwo, "Enum 2")]
		public void IsRepresenationEqual(TestEnum enumVal, string representation)
			=> Assert.Equal(representation, enumVal.GetRepresentation());

		[Theory]
		[InlineData(TestEnum.EnumOne | TestEnum.EnumTwo, "Enum 1", "Enum 2")]
		public void IsFlagRepresenationEqual(TestEnum enumVal, params string[] representation)
			=> Assert.Equal(enumVal.GetFlagRepresentation(), representation);

		[Theory]
		[InlineData(TestEnum.EnumOne, "Enum 1")]
		public void IsParseRepresenationEqual(TestEnum enumVal, string representation)
			=> Assert.Equal(representation.GetEnumFromRepresentation<TestEnum>(), enumVal);

		[Theory]
		[InlineData(TestByteEnum.EnumOne, "Enum 1")]
		public void IsByteParseRepresenationEqual(TestByteEnum enumVal, string representation)
			=> Assert.Equal(representation.GetEnumFromRepresentation<TestByteEnum>(), enumVal);

		[Theory]
		[InlineData(TestUShortEnum.EnumOne, "Enum 1")]
		public void IsUShortParseRepresenationEqual(TestUShortEnum enumVal, string representation)
			=> Assert.Equal(representation.GetEnumFromRepresentation<TestUShortEnum>(), enumVal);
	}
}
