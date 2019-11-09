using System.ComponentModel;
using System.Reflection;

public enum Location
{
	[Description(" the magical Noreki Forest\"," +
		"\" the impressive Forest of the Noreki\"," +
		"\" the mysterious Noreki Forest")]
	NorekiForest,
	[Description(" the majestic noble Castle, home to the royal family and their heirlooms\"," +
		"\" the impressive blue stone Castle, which has no equal in the known lands\"," +
		"\" the shining Castle, with a big fire at the top, reflecting all of its might\"," +
		"\" a mediocre Castle, if you ask me, where the king and queen live")]
	Castle,
	[Description(" the town that's so far away they call it Far Berry\"," +
		"\" the town where Tieflings abound, Far Berry\"," +
		"\" Far Berry, the home of Birokuma, a powerful spellcasting and talking bear\"," +
		"\" the land beyond all lands, Far Berry, governed by a giant woman and her friends, the other giant women")]
	FarBerry
}

public static class EnumHelper
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