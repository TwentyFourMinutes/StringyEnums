using System;
using System.Collections.Generic;

namespace StringyEnums
{
	public static class EnumExtensions
	{
		public static string GetRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
			=> EnumCore.RepresentationCache[typeof(TEnum)][(int)(dynamic)enumValue];

		public static bool TryGetRepresentation<TEnum>(this TEnum enumValue, out string? representation) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				return dict.TryGet((int)(dynamic)enumValue, out representation);
			}

			representation = default;
			return false;
		}

		public static string[] GetFlagRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
		{
			var dict = EnumCore.RepresentationCache[typeof(TEnum)];

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

		public static bool TryGetFlagRepresentation<TEnum>(this TEnum enumValue, out string[]? representations) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				var flags = (int)(dynamic)enumValue;

				representations = new string[GetHammingWeight(flags)];

				int lastIndex = 0;

				for (int i = 1; i <= flags; i *= 2)
				{
					if ((flags & i) != 0)
					{
						if (dict.TryGet(i, out var representation))
						{
							representations[lastIndex++] = representation;
						}
						else
						{
							return false;
						}
					}
				}

				return true;
			}

			representations = default;
			return false;
		}

		public static TEnum GetEnumFromRepresentation<TEnum>(this string val) where TEnum : struct, Enum
			=> (TEnum)(dynamic)EnumCore.RepresentationCache[typeof(TEnum)][val];

		public static bool TryGetEnumFromRepresentation<TEnum>(this string val, out TEnum enumResult) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				if (dict.TryGet(val, out var intVal))
				{
					enumResult = (TEnum)(dynamic)intVal;

					return true;
				}
			}

			enumResult = default;
			return false;
		}

		private static int GetHammingWeight(int value)
		{
			value -= ((value >> 1) & 0x55555555);
			value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
			return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		} 
	}
}