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
        internal static IDictionary<Type, IReadOnlyBiDictionary<uint, string>> RepresentationCache { get; }

        static EnumCore()
        {
            RepresentationCache = new Dictionary<Type, IReadOnlyBiDictionary<uint, string>>();
        }

        /// <summary>
        /// Initializes the <see cref="EnumCore"/> with the enums contained in the calling assembly.
        /// </summary>
        public static void Init()
            => InitFromCache(new CacheInitializer().InitWith(Assembly.GetCallingAssembly()));

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

            InitFromCache(cacheInit);
        }

        private static void InitFromCache(CacheInitializer cache)
        {
            foreach (var cacheItem in cache.CustructCache())
            {
                if (!RepresentationCache.ContainsKey(cacheItem.Key))
                    RepresentationCache.Add(cacheItem);
            }
        }
    }
}