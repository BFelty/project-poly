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
			Label sectionHeader = new();
			sectionHeader.Text += $"{section.SectionName}\n---";
			sectionHeader.AddThemeFontSizeOverride("font_size", 14);
			sectionHeader.HorizontalAlignment = HorizontalAlignment.Center;
			AddChild(sectionHeader);

			foreach (CreditEntry entry in section.Entries)
			{
				Label entryContent = new();
				foreach (PropertyInfo prop in entry.GetType().GetProperties())
				{
					string value = prop.GetValue(entry, null)?.ToString();
					if (value != null)
					{
						entryContent.Text += $"{value}\n";
					}
				}
				entryContent.AddThemeFontSizeOverride("font_size", 10);
				entryContent.AutowrapMode = TextServer.AutowrapMode.WordSmart;
				entryContent.HorizontalAlignment = HorizontalAlignment.Center;
				AddChild(entryContent);
			}
		}
	}
}
