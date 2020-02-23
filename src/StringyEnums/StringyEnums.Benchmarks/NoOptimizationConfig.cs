using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;

namespace StringyEnums.Benchmarks
{

    public class NoOptimizationConfig : ManualConfig
	{
		public NoOptimizationConfig()
		{
			Add(JitOptimizationsValidator.DontFailOnError);
			this.Options = ConfigOptions.DisableOptimizationsValidator;
		}
	}
}
