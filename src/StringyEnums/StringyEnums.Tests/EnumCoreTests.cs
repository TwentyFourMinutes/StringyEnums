using StringyEnums.Shared.Models;
using Xunit;

namespace StringyEnums.Tests
{
    public class EnumCoreTests
    {
        [Fact]
        public void IsEnumToRepresenationEqual()
        {
            EnumCoreExtensions.ClearCache();

            EnumCore.Init(cache => cache.InitWith<TestEnum>());

            Assert.Equal("Enum 4", TestEnum.EnumFour.GetRepresentation());

            EnumCore.Init(cache => cache.InitWith<TestUShortEnum>());

            Assert.Equal("Enum 4", TestUShortEnum.EnumFour.GetRepresentation());
        }

        [Fact]
        public void IsRepresenationToEnumEqual()
        {
            EnumCoreExtensions.ClearCache();

            EnumCore.Init(cache => cache.InitWith<TestEnum>());

            Assert.Equal(TestEnum.EnumFour, "Enum 4".GetEnumFromRepresentation<TestEnum>());

            EnumCore.Init(cache => cache.InitWith<TestUShortEnum>());

            Assert.Equal(TestUShortEnum.EnumFour, "Enum 4".GetEnumFromRepresentation<TestUShortEnum>());
        }
    }
}
