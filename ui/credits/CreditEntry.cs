namespace LastPolygon.Util.Credits;

#nullable enable
public class CreditEntry
{
	public required string Name { get; set; }
	public string? Author { get; set; }
	public string? Url { get; set; }
	public string? License { get; set; }
}
