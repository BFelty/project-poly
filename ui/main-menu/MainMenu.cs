using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;

public partial class MainMenu : CanvasLayer
{
	public override void _Ready()
	{
		AudioManager.Instance.ChangeMusic(Music.MusicTrack.EerieAmbience);
	}

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
