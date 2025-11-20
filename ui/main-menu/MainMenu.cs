using Godot;
using LastPolygon.Audio;
using LastPolygon.Globals;
using LastPolygon.UI.Credits;
using LastPolygon.UI.Tutorial;

namespace LastPolygon.UI;

public partial class MainMenu : CanvasLayer
{
	// Menu Scenes
	private PackedScene _creditsScene = GD.Load<PackedScene>(
		"uid://haajv6ak1h32"
	);
	private PackedScene _tutorialScene = GD.Load<PackedScene>(
		"uid://s3cr3xxq5ndh"
	);

	// Menu Nodes
	private VBoxContainer _centerMenu;
	private CreditsMenu _creditsMenu;
	private TutorialMenu _tutorialMenu;

	public override void _Ready()
	{
		AudioManager.Instance.ChangeMusic(Music.MusicTrack.EerieAmbience);

		_centerMenu = GetNode<VBoxContainer>("CenterContainer/VBoxContainer");
		_creditsMenu = _creditsScene.Instantiate<CreditsMenu>();
		AddChild(_creditsMenu);
		_tutorialMenu = _tutorialScene.Instantiate<TutorialMenu>();
		AddChild(_tutorialMenu);

		_creditsMenu.Hide();
		_tutorialMenu.Hide();

		_creditsMenu.CreditsMenuClosed += ToggleMenuVisibility;
		_tutorialMenu.TutorialMenuClosed += ToggleMenuVisibility;
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

	private void OnTutorialButtonPressed()
	{
		GD.Print("Tutorial button pressed!");
		ToggleMenuVisibility();
		_tutorialMenu.Show();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}

	private void ToggleMenuVisibility()
	{
		_centerMenu.Visible = !_centerMenu.Visible;
	}
}
