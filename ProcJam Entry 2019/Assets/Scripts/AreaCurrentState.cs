using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum AreaCurrentState 
{
	[Description(", and sadly, it is withering down\",\". It's now ridden by the green beasts")]
	NorekiForest,
	[Description(". Some attacks have lowered the castle's defenses, burglars have killed guards and some others have quit the job in fear\"," +
		"\". The royal family's shallow behavior has made it very complicated for guards. They're not nearly enough, and threats become worse every year, every day, every month\"," +
		"\", the weather has been harsh on the walls, too much rain, too much sand, and to make matters worse, thunder struck the back wall of the castle and tore a chunk of it down\"," +
		"\". In all honesty, the castle seemed fine at the time")]
	Castle,
	[Description(". Near Far Berry there was a spire of obsidian placed to mark the old war victories, which fell down without explanation\"," +
		"\". In this town, when the new moon arises, a ruined column of cristal follows. People gather around it every time\"," +
		"\". The town felt peaceful on those days, great weather, great mood. But something was weird\"," +
		"\", no one understood why the rising and falling was so important, but the river did. The river did")]
	FarBerry
}
