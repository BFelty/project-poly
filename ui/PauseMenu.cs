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
		// Toggles pause menu and SceneTree pause state when the ui_cancel
		// input is pressed
		// By default, this is the Escape key
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Paused = !GetTree().Paused;
			Visible = !Visible;
		}
	}

	// Only allows the resume button to unpause
	private void OnResumeButtonPressed()
	{
		GetTree().Paused = false;
		Visible = false;
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
