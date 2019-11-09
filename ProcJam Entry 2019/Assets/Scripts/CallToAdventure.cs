using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum CallToAdventure 
{
	[Description(". A party was gathered to solve the issue and so they went, with #hero# as their leader\", \". And to rescue the land, #hero# was recruited along his friends. So they went")]
	NorekiForest,
	[Description(", #hero# heard about an issue and went to the Castle to find out what the King needed\"," +
		"\". For #hero#, serving Royalty was a driving force, so he went to the Castle to achieve his dreams\"" +
		",\". Without hesitation, #hero# ran to the castle, planning to assist the Royals\"," +
		"\", because of that, #hero# ran towards the castle!\",\"... but in the end, #hero# was too slow and couldn't reach the castle on time")]
	Castle,
	[Description(". To try getting it over with, the chief found #hero# on a random trip and pushed them to adventure\"," +
		"\". Without a choice, #hero# being the only capable person in town at the time of the crisis, they had to step up\"," +
		"\". An inside job, according to the chieftain. There was only one way to solve it - an outsider, like #hero#. The craziness started immediately\"," +
		"\", the trip there hadn't been short, and #hero# was still quite tired, but there was no choice but to help")]
	FarBerry
}
