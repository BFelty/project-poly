using Godot;
using LastPolygon.UI;

namespace LastPolygon.Globals;

public partial class UIManager : Control
{
	private PackedScene _pauseMenuScene = GD.Load<PackedScene>(
		"uid://b67ser1howvws"
	);
	public PauseMenu PauseMenu { get; private set; }
	public static UIManager Instance { get; private set; }

	public override void _Ready()
	{
		PauseMenu = _pauseMenuScene.Instantiate() as PauseMenu;
		Instance = this;
	}
}
