using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Godot;

namespace LastPolygon.UI;

public partial class Credits : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string creditsJson = File.ReadAllText("ui/credits/credits.json");
		List<Section> credits = ParseJson(creditsJson);
	}

	private void OnBackButtonPressed()
	{
		Hide();
	}

	private List<Section> ParseJson(string json)
	{
		JsonSerializerOptions options = new()
		{
			PropertyNameCaseInsensitive = true,
		};

		return JsonSerializer.Deserialize<List<Section>>(json, options);
	}

	private void GenerateCredits()
	{
		// TODO - Automatically populate credits from markdown or JSON file
	}
}
