using Godot;
using LastPolygon.Globals;

public partial class MainMenu : CanvasLayer
{
	private void OnPlayButtonPressed()
	{
		// Use SceneManager to change the scene

		//GD.Print("Main menu emit signal: " + SignalBus.SignalName.GameStarted);
		//SignalBus.Instance.EmitSignal(SignalBus.SignalName.GameStarted);
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	}
}
