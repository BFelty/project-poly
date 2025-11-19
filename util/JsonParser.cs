using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using Godot;
using LastPolygon.Util.Credits;

namespace LastPolygon.Util;

public partial class JsonParser : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string creditsJson = FileAccess
			.Open("res://ui/credits/credits.json", FileAccess.ModeFlags.Read)
			?.GetAsText();
		List<CreditSection> credits = ParseJson(creditsJson);
		GenerateCreditsUi(credits);
	}

	public List<CreditSection> ParseJson(string json)
	{
		JsonSerializerOptions options = new()
		{
			PropertyNameCaseInsensitive = true,
		};

		return JsonSerializer.Deserialize<List<CreditSection>>(json, options);
	}

	public void GenerateCreditsUi(List<CreditSection> credits)
	{
		// TODO - Automatically populate credits from markdown or JSON file
		foreach (CreditSection section in credits)
		{
			// Display SectionName appropriately
			GD.Print(section.SectionName);

			// Possibly implement spacing
			GD.Print("---");

			foreach (CreditEntry entry in section.Entries)
			{
				foreach (PropertyInfo prop in entry.GetType().GetProperties())
				{
					GD.Print(
						prop.Name + " " + prop.GetValue(entry, null)?.ToString()
					);
				}
				// Display each key-value pair
				/*string name = entry.Name;
				string author = entry.Author;
				string url = entry.Url;
				string license = entry.License;

				GD.Print(
					$"{name}\nAuthor: {author}\nURL: {url}\nLicense: {license}"
				);*/
				// Spacing between entries
			}
		}
	}
}
