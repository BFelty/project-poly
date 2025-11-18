using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class MainMenu : CanvasLayer
{
	// Menu Scenes
	PackedScene _creditsScene = GD.Load<PackedScene>("uid://haajv6ak1h32");

	// Menu Nodes
	Credits _credits;

	public override void _Ready()
	{
		AudioManager.Instance.ChangeMusic(Music.MusicTrack.EerieAmbience);

		_credits = _creditsScene.Instantiate<Credits>();
		AddChild(_credits);
		_credits.Hide();
	}

	private void OnPlayButtonPressed()
	{
		// Use SceneManager to change the scene
		GD.Print("Play button pressed!");
		SceneManager.Instance.ChangeScene(SceneManager.Instance.GameScene);
	}

	private void OnCreditsButtonPressed()
	{
		GD.Print("Credits button pressed!");
		_credits.Show();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
