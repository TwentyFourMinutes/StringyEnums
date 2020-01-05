using BidirectionalDict;
using StringyEnums.Shared.Models;
using Xunit;

namespace StringyEnums.Test
{
	public class EnumExtensionTests
	{
		public EnumExtensionTests()
		{
			EnumCore.Init(init => init.InitWith<TestEnum>());
		}

		[Theory]
		[InlineData(TestEnum.EnumOne, "Enum 1")]
		[InlineData(TestEnum.EnumTwo, "Enum 2")]
		public void IsRepresenationEqual(TestEnum enumVal, string representation)
			=> Assert.Equal(representation, enumVal.GetStringRepresentation());

		[Theory]
		[InlineData(TestEnum.EnumOne | TestEnum.EnumTwo, "Enum 1", "Enum 2")]
		public void IsFlagRepresenationEqual(TestEnum enumVal, params string[] representation)
			=> Assert.Equal(enumVal.GetFlagStringRepresentation(), representation);

		[Theory]
		[InlineData(TestEnum.EnumOne, "Enum 1")]
		public void IsParseRepresenationEqual(TestEnum enumVal, string representation)
			=> Assert.Equal(representation.GetEnumFromRepresentation<TestEnum>(), enumVal);
	}
}
