using System.Collections.Generic;

namespace LastPolygon.Util.Credits;

public class CreditSection
{
	public required string SectionName { get; set; }
	public required List<CreditEntry> Entries { get; set; }
}
