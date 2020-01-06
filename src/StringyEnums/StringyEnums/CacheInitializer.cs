using BidirectionalDict;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace StringyEnums
{
	/// <summary>
	/// Handles the initializing of a Cache 
	/// </summary>
	public class CacheInitializer
	{
		private readonly Dictionary<Type, IReadOnlyBiDictionary<int, string>> _tempCache;

		internal CacheInitializer()
		{
			_tempCache = new Dictionary<Type, IReadOnlyBiDictionary<int, string>>();
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
					var fields = enumType.GetFields();

					var temptDict = new BiDictionary<int, string>();

					foreach (var field in fields.Where(x => x.IsStatic))
					{
						var attr = field.GetCustomAttribute<StringRepresentationAttribute>();

						if (attr is { })
						{
							temptDict.TryAdd((int)field.GetValue(enumType)!, attr.StringRepresentation);
						}
					}

					if (temptDict.Count > 0)
					{
						_tempCache.Add(enumType, temptDict);
					}
				}
			}

			return this;
		}

		public CacheInitializer InitWith<TEnum>() where TEnum : struct, Enum
		{
			var enumType = typeof(TEnum);

			var fields = enumType.GetFields();

			var temptDict = new BiDictionary<int, string>();

			foreach (var field in fields.Where(x => x.IsStatic))
			{
				var attr = field.GetCustomAttribute<StringRepresentationAttribute>();

				if (attr is { })
				{
					temptDict.TryAdd((int)field.GetValue(enumType)!, attr.StringRepresentation);
				}
			}

			if (temptDict.Count > 0)
			{
				_tempCache.Add(enumType, temptDict);
			}

			return this;
		}

		internal IReadOnlyDictionary<Type, IReadOnlyBiDictionary<int, string>> CustructCache()
			=> new ReadOnlyDictionary<Type, IReadOnlyBiDictionary<int, string>>(_tempCache.ToDictionary(k => k.Key, v => (IReadOnlyBiDictionary<int, string>)new ReadOnlyBiDictionary<int, string>(v.Value)));
	}
}
