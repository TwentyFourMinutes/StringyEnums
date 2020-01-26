using BenchmarkDotNet.Running;
using System.Reflection;

namespace StringyEnums.Benchmarks
{
	public class Startup
	{
		public static void Main()
			=> BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly())
								.RunAll(new NoOptimizationConfig());
	}
}
