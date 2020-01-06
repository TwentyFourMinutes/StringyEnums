using BidirectionalDict;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StringyEnums
{
	public static class EnumCore
	{
		private static IReadOnlyDictionary<Type, IReadOnlyBiDictionary<int, string>>? _representationCache;

		internal static IReadOnlyDictionary<Type, IReadOnlyBiDictionary<int, string>> RepresentationCache
		{
			get => _representationCache ?? throw new NotInitializedException("Please call the EnumCore.Initialize method at application startup.");
			private set => _representationCache = value;
		}

		public static void Init()
		{
			RepresentationCache = new CacheInitializer().InitWith(Assembly.GetCallingAssembly()).CustructCache();
		}

		public static void Init(Action<CacheInitializer> initializer, bool includeCallingAssembly = true)
		{
			var cacheInit = new CacheInitializer();

			if (includeCallingAssembly)
				cacheInit.InitWith(Assembly.GetCallingAssembly());

			initializer?.Invoke(cacheInit);

			RepresentationCache = cacheInit.CustructCache();
		}
	}
}
