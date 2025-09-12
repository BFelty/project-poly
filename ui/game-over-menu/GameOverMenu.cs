using Godot;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class GameOverMenu : CanvasLayer
{
	public override void _Ready()
	{
		Visible = false;
	}

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
		// Use SceneManager autoload to change scene
		SceneManager.Instance.ChangeScene(SceneManager.Instance.MainMenuScene);
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
