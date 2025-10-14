using Godot;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class GameOverMenu : CanvasLayer
{
	private Label _timeSurvivedLabel;

	public override void _Ready()
	{
		// Connect to global events
		EventBus.GameEnded += UpdateRunStats;

		Visible = false;
		_timeSurvivedLabel = FindChild("TimeSurvivedLabel") as Label;
	}

	public override void _ExitTree()
	{
		// Disconnect from global events
		EventBus.GameEnded -= UpdateRunStats;
	}

	private void UpdateRunStats(int finalWave, float timeSurvived)
	{
		_timeSurvivedLabel.Text =
			$"You made it to Wave {finalWave} and\n"
			+ $"survived {timeSurvived} seconds.";
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
