using Godot;
using LastPolygon.UI;

namespace LastPolygon.Globals;

public partial class UIManager : Control
{
	private PackedScene _pauseMenuScene = GD.Load<PackedScene>(
		"uid://n1y0r7o4fsq0"
	);
	private PackedScene _gameOverScene = GD.Load<PackedScene>(
		"uid://bekdrbn7k6a"
	);

	public PauseMenu PauseMenu { get; private set; }

	public static UIManager Instance { get; private set; }

	public override void _Ready()
	{
		PauseMenu = _pauseMenuScene.Instantiate() as PauseMenu;
		Instance = this;
	}
}
