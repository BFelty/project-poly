using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace LastPolygon.UI;

public partial class Credits : CanvasLayer
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

	private void OnBackButtonPressed()
	{
		Hide();
	}

	private List<CreditSection> ParseJson(string json)
	{
		JsonSerializerOptions options = new()
		{
			PropertyNameCaseInsensitive = true,
		};

		return JsonSerializer.Deserialize<List<CreditSection>>(json, options);
	}

	private void GenerateCreditsUi(List<CreditSection> credits)
	{
		// TODO - Automatically populate credits from markdown or JSON file
	}
}
