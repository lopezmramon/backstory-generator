using System.ComponentModel;
using System.Reflection;

public enum Gender
{
	[Description("of unknown gender")]
    NotGiven,
	[Description("guy")]
	Male,
	[Description("girl")]
	Female,
	[Description("")]
	Other,
}

public static class GenderHelper
{
	public static string GetEnumDescription(System.Enum value)
	{
		FieldInfo fi = value.GetType().GetField(value.ToString());

		DescriptionAttribute[] attributes =
			(DescriptionAttribute[])fi.GetCustomAttributes(
			typeof(DescriptionAttribute),
			false);

		if (attributes != null &&
			attributes.Length > 0)
			return attributes[0].Description;
		else
			return value.ToString();
	}

}
