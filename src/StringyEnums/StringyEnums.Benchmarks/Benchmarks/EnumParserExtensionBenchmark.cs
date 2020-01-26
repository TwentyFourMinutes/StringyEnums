using BenchmarkDotNet.Attributes;

namespace StringyEnums.Benchmarks
{
	[MemoryDiagnoser]
	[Config(typeof(NoOptimizationConfig))]
	public class EnumParserExtensionBenchmark
	{
		[Benchmark]
		public BenchmarkEnum ParseToEnum()
			=> (BenchmarkEnum)EnumParser.EnumParser.Parse(typeof(BenchmarkEnum),"Value 5");
	}
}
