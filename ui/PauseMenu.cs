using Godot;

namespace LastPolygon.UI;

public partial class PauseMenu : CanvasLayer
{
	public override void _Ready()
	{
		Visible = false;
	}

	public override void _Input(InputEvent @event)
	{
		// Pause the main SceneTree when the ui_cancel is pressed
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			Visible = !Visible;
		}
	}

	// TODO - Create desired pause menu functionality
	private void OnResumeButtonPressed()
	{
		GD.Print("Resume button pressed!");
	}

	private void OnQuitButtonPressed()
	{
		GD.Print("Quit button pressed");
	}
}
