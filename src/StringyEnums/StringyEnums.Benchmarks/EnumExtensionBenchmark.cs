using BenchmarkDotNet.Attributes;
using StringyEnums.Shared.Models;

namespace StringyEnums.Benchmarks
{
	[MemoryDiagnoser]
	public class EnumExtensionBenchmark
	{
		[GlobalSetup]
		public void Setup()
		{
			EnumCore.Init(init => init.InitWith<TestEnum>());
		}

		[Benchmark]
		public TEnum DynamicCast()
		{
			return (TEnum)(dynamic)0;
		}

		[Benchmark]
		public TEnum ObjectCast()
		{
			return (TEnum)(object)0;
		}

		[Benchmark]
		public TEnum DirectCast()
		{
			return (TEnum)0;
		}

		public enum TEnum
		{
			Foo,
			Bar
		}
	}
}
