using Godot;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class GameOverMenu : CanvasLayer
{
	private Label _timeSurvivedLabel;

	public override void _Ready()
	{
		// Connect to global signals
		SignalBus.Instance.GameEnded += SetTimeSurvived;

		Visible = false;
		_timeSurvivedLabel = FindChild("TimeSurvivedLabel") as Label;
	}

	public override void _ExitTree()
	{
		// Disconnect from global signals
		SignalBus.Instance.GameEnded -= SetTimeSurvived;
	}

	private void SetTimeSurvived(float timeSurvived)
	{
		_timeSurvivedLabel.Text = $"You survived {timeSurvived} seconds.";
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
