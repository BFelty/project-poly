using Godot;
using LastPolygon.Players;

namespace LastPolygon.Globals;

public partial class SignalBus : Node
{
	[Signal]
	public delegate void PlayerPickupCollectedEventHandler(
		Vector2 collidedPlayerPosition
	);

	[Signal]
	public delegate void PlayerDamagedEventHandler(Player player);

	[Signal]
	public delegate void PlayerDiedEventHandler(int playerCount);

	public static SignalBus Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
