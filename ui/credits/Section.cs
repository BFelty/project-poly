using System.Collections.Generic;

namespace LastPolygon.UI;

public class Section
{
	public required string SectionName { get; set; }
	public required List<Entry> Entries { get; set; }
}
