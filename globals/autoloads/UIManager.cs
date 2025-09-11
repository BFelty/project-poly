using Godot;

namespace LastPolygon.Globals;

public partial class UIManager : Control
{
	private PackedScene _pauseMenuScene = GD.Load<PackedScene>(
		"uid://b67ser1howvws"
	);
	public static UIManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
