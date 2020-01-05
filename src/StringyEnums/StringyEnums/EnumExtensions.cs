using System;
using System.Collections.Generic;

namespace StringyEnums
{
	public static class EnumExtensions
	{
		public static string GetRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				if (dict.TryGet((int)(dynamic)enumValue, out var representation))
				{
					return representation;
				}
			}

			throw new InvalidOperationException("The enum passed, isn't in any assemblies passed to the EnumCore.Init method.");
		}

		public static string[] GetFlagRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				var flags = (int)(dynamic)enumValue;

				var tempArr = new string[GetHammingWeight(flags)];

				int lastIndex = 0;

				for (int i = 1; i <= flags; i *= 2)
				{
					if ((flags & i) != 0)
					{
						tempArr[lastIndex++] = dict[i];
					}
				}
				
				return tempArr;
			}
			else
			{
				throw new InvalidOperationException("The enum passed, isn't in the assembly passed to the EnumCore.InitWith method.");
			}
		}

		public static TEnum GetEnumFromRepresentation<TEnum>(this string val) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				dict.TryGet(val, out var intVal);

				return (TEnum)(dynamic)(intVal);
			}
			else
			{
				throw new InvalidOperationException($"Couldn't parse '{val}' to {nameof(TEnum)} the enum wasn't in any assemblies passed to the EnumCore.InitWith method.");
			}
		}

		private static int GetHammingWeight(int value)
		{
			value -= ((value >> 1) & 0x55555555);
			value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
			return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}
	}
}
