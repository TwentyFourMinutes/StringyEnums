using System;
using System.Collections.Generic;

namespace StringyEnums
{
	/// <summary>
	/// Provides useful extension methods to retrieve the string representation of a bool and methods to parse string representations to bools.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Gets the string representation from the <see cref="StringRepresentationAttribute"/> on the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="enumValue">The enum member which the string representation should get retrieved from.</param>
		/// <returns>The string representation of the given <paramref name="enumValue"/>.</returns>
		/// <exception cref="KeyNotFoundException">Is thrown when the given <paramref name="enumValue"/> doesn't contain any string representation or the <typeparamref name="TEnum"/> isn't cached.</exception>
		public static string GetRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
			=> EnumCore.RepresentationCache[typeof(TEnum)][(uint)(dynamic)enumValue];

		/// <summary>
		/// Tries to get the string representation from the <see cref="StringRepresentationAttribute"/> on the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="enumValue">The enum member which the string representation should get retrieved from.</param>
		/// <param name="representation">The string representation of the given <paramref name="enumValue"/>.</param>
		/// <returns>A bool which indicates whether the string representation could be retrieved or not.</returns>
		public static bool TryGetRepresentation<TEnum>(this TEnum enumValue, out string? representation) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				return dict.TryGet((uint)(dynamic)enumValue, out representation);
			}

			representation = default;
			return false;
		}

		/// <summary>
		/// Gets the string representation from the from the <see cref="StringRepresentationAttribute"/> on all the given <typeparamref name="TEnum"/> flags.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="enumValue">The enum which the string representations should get retrieved from.</param>
		/// <returns>All string representations of the given <paramref name="enumValue"/> flags.</returns>
		/// <exception cref="KeyNotFoundException">Is thrown when one of given <paramref name="enumValue"/> flags doesn't contain any string representation or the <typeparamref name="TEnum"/> isn't cached.</exception>
		public static string[] GetFlagRepresentation<TEnum>(this TEnum enumValue) where TEnum : struct, Enum
		{
			var dict = EnumCore.RepresentationCache[typeof(TEnum)];

			var flags = (uint)(dynamic)enumValue;

			var tempArr = new string[GetHammingWeight(flags)];

			int lastIndex = 0;

			for (uint i = 1; i <= flags; i *= 2)
			{
				if ((flags & i) != 0)
				{
					tempArr[lastIndex++] = dict[i];
				}
			}

			return tempArr;
		}

		/// <summary>
		/// Tries to get the string representation from the from the <see cref="StringRepresentationAttribute"/> on all the given <typeparamref name="TEnum"/> flags.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="enumValue">The enum which the string representations should get retrieved from.</param>
		/// <param name="representations">The string representations of the given <paramref name="enumValue"/> flags.</param>
		/// <returns>A bool which indicates whether all string representation could be retrieved or not.</returns>
		public static bool TryGetFlagRepresentation<TEnum>(this TEnum enumValue, out string[]? representations) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				var flags = (uint)(dynamic)enumValue;

				representations = new string[GetHammingWeight(flags)];

				int lastIndex = 0;

				for (uint i = 1; i <= flags; i *= 2)
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

		/// <summary>
		/// Gets the <typeparamref name="TEnum"/> from the string representation based on the <see cref="StringRepresentationAttribute"/> of the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="value">The string representation from which the <typeparamref name="TEnum"/> will get retrieved.</param>
		/// <returns>The <typeparamref name="TEnum"/> member from the given <paramref name="value"/>.</returns>
		/// <exception cref="KeyNotFoundException">Is thrown when no matching <typeparamref name="TEnum"/> member was found for the given <paramref name="value"/> or the <typeparamref name="TEnum"/> isn't cached.</exception>

		public static TEnum GetEnumFromRepresentation<TEnum>(this string value) where TEnum : struct, Enum
			=> (TEnum)(dynamic)EnumCore.RepresentationCache[typeof(TEnum)][value];

		/// <summary>
		/// Tries to get the <typeparamref name="TEnum"/> from the string representation based on the <see cref="StringRepresentationAttribute"/> of the given <typeparamref name="TEnum"/>.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="value">The string representation from which the <typeparamref name="TEnum"/> will get retrieved.</param>
		/// <param name="enumResult">The <typeparamref name="TEnum"/> member from the given <paramref name="value"/>.</param>
		/// <returns>A bool which indicates whether a <typeparamref name="TEnum"/> member from the given string representation could be retrieved or not.</returns>
		public static bool TryGetEnumFromRepresentation<TEnum>(this string value, out TEnum enumResult) where TEnum : struct, Enum
		{
			if (EnumCore.RepresentationCache.TryGetValue(typeof(TEnum), out var dict))
			{
				if (dict.TryGet(value, out var intVal))
				{
					enumResult = (TEnum)(dynamic)intVal;

					return true;
				}
			}

			enumResult = default;
			return false;
		}

		private static uint GetHammingWeight(uint value)
		{
			value -= ((value >> 1) & 0x55555555);
			value = (value & 0x33333333) + ((value >> 2) & 0x33333333);
			return (((value + (value >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24;
		}
	}
}