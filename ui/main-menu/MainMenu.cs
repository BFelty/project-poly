using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;
using LastPolygon.UI.Credits;

namespace LastPolygon.UI;

public partial class MainMenu : CanvasLayer
{
	// Menu Scenes
	PackedScene _creditsScene = GD.Load<PackedScene>("uid://haajv6ak1h32");

	// Menu Nodes
	CreditsMenu _creditsMenu;

	public override void _Ready()
	{
		AudioManager.Instance.ChangeMusic(Music.MusicTrack.EerieAmbience);

		_creditsMenu = _creditsScene.Instantiate<CreditsMenu>();
		AddChild(_creditsMenu);
		_creditsMenu.Hide();
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
		_creditsMenu.Show();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
