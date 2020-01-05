using System;

namespace StringyEnums
{
	[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
	public sealed class StringRepresentationAttribute : Attribute
	{
		internal readonly string StringRepresentation;

		public StringRepresentationAttribute(string stringRepresentation)
		{
			StringRepresentation = stringRepresentation;
		}
	}
}
