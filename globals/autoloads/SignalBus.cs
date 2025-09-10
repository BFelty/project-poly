using Godot;

namespace LastPolygon.Globals;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void PlayerPickupCollectedEventHandler(
		Vector2 collidedPlayerPosition
	);

	public static SignalBus Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
