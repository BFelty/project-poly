using Godot;

public partial class GameOverMenu : CanvasLayer
{
	public override void _Ready()
	{
		Visible = false;
	}

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
