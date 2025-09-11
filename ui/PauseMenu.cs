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
		// Pause the main SceneTree when the ui_cancel input is pressed
		// By default, this is the Escape key
		if (@event.IsActionPressed("ui_cancel"))
		{
			TogglePause();
		}
	}

	// TODO - Create desired pause menu functionality
	private void OnResumeButtonPressed()
	{
		TogglePause();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	private void TogglePause()
	{
		GetTree().Paused = !GetTree().Paused;
		Visible = !Visible;
	}
}
