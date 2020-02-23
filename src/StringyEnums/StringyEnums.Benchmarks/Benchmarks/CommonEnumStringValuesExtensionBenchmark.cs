using BenchmarkDotNet.Attributes;
using Common.EnumStringValues;

namespace StringyEnums.Benchmarks
{
    [MemoryDiagnoser]
	[Config(typeof(NoOptimizationConfig))]
	public class CommonEnumStringValuesExtensionBenchmark
	{
		[Benchmark]
		public string GetStringValue()
			=> BenchmarkEnum.Val5.GetStringValue();

		[Benchmark]
		public BenchmarkEnum ParseToEnum()
			=> "Value 5".GetEnumValue<BenchmarkEnum>();
	}
}
