using BidirectionalDict;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StringyEnums
{
    /// <summary>
    /// Handles the initializing of a Cache
    /// </summary>
    public class CacheInitializer
    {
        private readonly Dictionary<Type, IReadOnlyBiDictionary<uint, string>> _tempCache;

        internal CacheInitializer()
        {
            _tempCache = new Dictionary<Type, IReadOnlyBiDictionary<uint, string>>();
        }

        /// <summary>
        /// Initializes the cache a given set of assemblies.
        /// </summary>
        /// <param name="assemblies">Assemblies which contain enums which get added to the cache.</param>
        public CacheInitializer InitWith(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var enumTypes = assembly.GetTypes().Where(x => x.IsEnum);

                foreach (var enumType in enumTypes)
                {
                    InitWith(enumType);
                }
            }

            return this;
        }

        /// <summary>
        /// Initializes the cache with a given enum.
        /// </summary>
        /// <typeparam name="TEnum">The enum which should be added to the cache.</typeparam>
        public CacheInitializer InitWith<TEnum>() where TEnum : struct, Enum
        {
            InitWith(typeof(TEnum));

            return this;
        }

        /// <summary>
        /// Initializes the cache with a set of given enums.
        /// </summary>
        /// <param name="enums">The enums which should be added to the cache.</param>
        public CacheInitializer InitWith(params Enum[] enums)
        {
            foreach (var enumVal in enums)
            {
                InitWith(enumVal);
            }

            return this;
        }

        private void InitWith(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException("The type provided is not of type enum", nameof(enumType));

            var fields = enumType.GetFields();

            var temptDict = new BiDictionary<uint, string>();

            foreach (var field in fields.Where(x => x.IsStatic))
            {
                var attr = field.GetCustomAttribute<StringRepresentationAttribute>();

                if (attr is { })
                {
                    temptDict.TryAdd(Convert.ToUInt32(field.GetValue(enumType))!, attr.StringRepresentation);
                }
            }

            if (temptDict.Count > 0)
            {
                _tempCache.Add(enumType, temptDict);
            }
        }

        internal IEnumerable<KeyValuePair<Type, IReadOnlyBiDictionary<uint, string>>> CustructCache()
        {
            foreach (var cacheItem in _tempCache)
            {
                yield return new KeyValuePair<Type, IReadOnlyBiDictionary<uint, string>>(cacheItem.Key, new ReadOnlyBiDictionary<uint, string>(cacheItem.Value));
            }
        }
    }
}
