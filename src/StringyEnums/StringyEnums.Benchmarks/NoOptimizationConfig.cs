using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Validators;
using System;
using System.Collections.Generic;
using System.Text;

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
