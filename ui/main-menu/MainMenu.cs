using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;
using LastPolygon.UI.Credits;

namespace LastPolygon.UI;

public partial class MainMenu : CanvasLayer
{
	// Menu Scenes
	private PackedScene _creditsScene = GD.Load<PackedScene>(
		"uid://haajv6ak1h32"
	);

	// Menu Nodes
	private VBoxContainer _centerMenu;
	private CreditsMenu _creditsMenu;

	public override void _Ready()
	{
		AudioManager.Instance.ChangeMusic(Music.MusicTrack.EerieAmbience);

		_centerMenu = GetNode<VBoxContainer>("CenterContainer/VBoxContainer");
		_creditsMenu = _creditsScene.Instantiate<CreditsMenu>();
		AddChild(_creditsMenu);

		_creditsMenu.Hide();

		_creditsMenu.CreditsMenuClosed += ToggleMenuVisibility;
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
		ToggleMenuVisibility();
		_creditsMenu.Show();
	}

	private void ToggleMenuVisibility()
	{
		_centerMenu.Visible = !_centerMenu.Visible;
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
