using Godot;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class GameOverMenu : CanvasLayer
{
	public override void _Ready()
	{
		Visible = false;
	}

	// TODO - Create game over screen functionality
	private void OnRestartButtonPressed()
	{
		// Emit signal indicating the game has started
		GD.Print(
			"Game over menu emit signal: " + SignalBus.SignalName.GameStarted
		);
		SignalBus.Instance.EmitSignal(SignalBus.SignalName.GameStarted);
	}

	private void OnMainMenuButtonPressed()
	{
		// TODO - Emit signal indicating the user wants to return to the main menu
		GD.Print("Main menu button pressed!");
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
