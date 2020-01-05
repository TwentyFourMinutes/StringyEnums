using BenchmarkDotNet.Running;
using System.Reflection;

namespace StringyEnums.Benchmarks
{
	public class Startup
	{
		public static void Main(string[] args)
			=> BenchmarkRunner.Run(Assembly.GetExecutingAssembly());
	}
}
