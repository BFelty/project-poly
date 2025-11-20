using System.Collections.Generic;

namespace LastPolygon.Util.Tutorial;

public class TutorialSection
{
	public required string SectionName { get; set; }
	public required List<TutorialEntry> Entries { get; set; }
}
