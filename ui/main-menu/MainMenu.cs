using Godot;
using LastPolygon.Globals;

public partial class MainMenu : CanvasLayer
{
	private void OnPlayButtonPressed()
	{
		// Use SceneManager to change the scene
		GD.Print("Play button pressed!");
		SceneManager.Instance.ChangeScene(SceneManager.Instance.GameScene);
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
