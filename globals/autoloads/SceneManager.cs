using Godot;
using LastPolygon.UI;

namespace LastPolygon.Globals;

public partial class SceneManager : Control
{
	public static SceneManager Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
