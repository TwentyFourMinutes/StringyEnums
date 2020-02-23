using System;

namespace StringyEnums
{
	/// <summary>
	/// Defines the string representation for an enum member.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class StringRepresentationAttribute : Attribute
	{
		internal readonly string StringRepresentation;

		/// <summary>
		/// Sets the string representation for an enum member.
		/// </summary>
		/// <param name="stringRepresentation">The string representation which should be set-</param>
		public StringRepresentationAttribute(string stringRepresentation)
		{
			StringRepresentation = stringRepresentation;
		}
	}
}
