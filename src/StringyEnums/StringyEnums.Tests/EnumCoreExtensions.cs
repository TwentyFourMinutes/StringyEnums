using System.Reflection;

namespace StringyEnums.Tests
{
    public static class EnumCoreExtensions
    {
        public static void ClearCache()
            => ((dynamic)typeof(EnumCore).GetProperty("RepresentationCache", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null)).Clear();
    }
}
