using System.Collections.Generic;
using System.Linq;
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
		foreach (CreditSection section in credits)
		{
			// Display SectionName appropriately
			RichTextLabel sectionHeader = new();
			sectionHeader.Text += $"{section.SectionName}\n---";
			sectionHeader.AddThemeFontSizeOverride("normal_font_size", 14);
			sectionHeader.FitContent = true;
			sectionHeader.HorizontalAlignment = HorizontalAlignment.Center;
			AddChild(sectionHeader);

			foreach (CreditEntry entry in section.Entries)
			{
				RichTextLabel entryContent = new();

				entryContent.Text =
					string.Join(
						"\n",
						entry
							.GetType()
							.GetProperties()
							.Select(p => p.GetValue(entry, null)?.ToString())
							.Where(v => v is not null)
					) + "\n\n";

				entryContent.AddThemeFontSizeOverride("normal_font_size", 10);
				entryContent.FitContent = true;
				entryContent.HorizontalAlignment = HorizontalAlignment.Center;
				entryContent.SelectionEnabled = true;
				AddChild(entryContent);
			}
		}
	}
}
