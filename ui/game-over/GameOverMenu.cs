using Godot;

public partial class GameOverMenu : CanvasLayer
{
	// TODO - Create game over screen functionality
	private void OnRestartButtonPressed()
	{
		GD.Print("Restart button pressed!");
	}

	private void OnMainMenuButtonPressed()
	{
		GD.Print("Main menu button pressed!");
	}

	private void OnQuitButtonPressed()
	{
		GD.Print("Quit button pressed!");
	}
}
