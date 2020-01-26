using BenchmarkDotNet.Attributes;

namespace StringyEnums.Benchmarks
{
	[MemoryDiagnoser]
	[Config(typeof(NoOptimizationConfig))]
	public class StringyEnumExtensionBenchmark
	{
		[GlobalSetup]
		public void Setup()
		{
			EnumCore.Init();
		}

		[Benchmark]
		public string GetStringValue()
			=> BenchmarkEnum.Val5.GetRepresentation();

		[Benchmark]
		public BenchmarkEnum ParseToEnum()
			=> "Value 5".GetEnumFromRepresentation<BenchmarkEnum>();
	}
}
