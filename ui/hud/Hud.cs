using Godot;
using LastPolygon.Globals;

namespace LastPolygon.UI;

public partial class Hud : CanvasLayer
{
	private Label _waveLabel;

	public override void _Ready()
	{
		// Connect to global events
		EventBus.WaveCompleted += UpdateWave;

		// Get references to nodes
		_waveLabel = FindChild("WaveLabel") as Label;
	}

	public override void _ExitTree()
	{
		// Disconnect from global events to prevent errors
		EventBus.WaveCompleted -= UpdateWave;
	}

	private void UpdateWave(int currentWave)
	{
		_waveLabel.Text = $"Wave {currentWave}";
	}

	public void StopUpdating()
	{
		EventBus.WaveCompleted -= UpdateWave;
	}
}
