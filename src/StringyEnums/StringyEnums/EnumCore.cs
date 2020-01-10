using BidirectionalDict;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace StringyEnums
{
	/// <summary>
	/// Handles caching of enums which have the <see cref="StringRepresentationAttribute"/>.
	/// </summary>
	public static class EnumCore
	{
		private static IReadOnlyDictionary<Type, IReadOnlyBiDictionary<uint, string>>? _representationCache;

		internal static IReadOnlyDictionary<Type, IReadOnlyBiDictionary<uint, string>> RepresentationCache
		{
			get => _representationCache ?? throw new NotInitializedException("Please call the EnumCore.Initialize method at application startup.");
			private set => _representationCache = value;
		}

		/// <summary>
		/// Initializes the <see cref="EnumCore"/> with the enums contained in the calling assembly.
		/// </summary>
		public static void Init()
		{
			RepresentationCache = new CacheInitializer().InitWith(Assembly.GetCallingAssembly()).CustructCache();
		}

		/// <summary>
		/// Initializes the <see cref="EnumCore"/> with the enums provided by the <paramref name="initializer"/>.
		/// </summary>
		/// <param name="initializer">Provides a <see cref="CacheInitializer"/> instance where assemblies with enums can be added.</param>
		/// <param name="includeCallingAssembly">Indicates whether the calling assembly should be searched for enums or not.</param>
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