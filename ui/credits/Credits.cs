using Godot;

namespace LastPolygon.UI;

public partial class Credits : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GenerateCredits();
	}

	private void OnBackButtonPressed()
	{
		Hide();
	}

	private void GenerateCredits()
	{
		// TODO - Automatically populate credits from markdown or JSON file
	}
}
