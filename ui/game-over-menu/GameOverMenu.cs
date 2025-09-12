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
		// Reload the Game scene
		GetTree().ReloadCurrentScene();
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
