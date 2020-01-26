using BenchmarkDotNet.Attributes;
using EnumStringValues;

namespace StringyEnums.Benchmarks
{
	[MemoryDiagnoser]
	[Config(typeof(NoOptimizationConfig))]
	public class EnumStringValuesExtensionBenchmark
	{
		[GlobalSetup]
		public void Setup()
		{
			EnumStringValues.EnumExtensions.Behaviour.UseCaching = true;

			BenchmarkEnum.Val5.GetStringValue();
			"Value 5".ParseToEnum<BenchmarkEnum>();
		}

		[Benchmark]
		public string GetStringValue()
			=> BenchmarkEnum.Val5.GetStringValue();

		[Benchmark]
		public BenchmarkEnum ParseToEnum()
			=> "Value 5".ParseToEnum<BenchmarkEnum>();
	}
}
