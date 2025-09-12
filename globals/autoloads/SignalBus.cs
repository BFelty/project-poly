using Godot;
using LastPolygon.Players;

namespace LastPolygon.Globals;

public partial class SignalBus : Node
{
	// Player Signals
	[Signal]
	public delegate void PlayerDamagedEventHandler(Player player);

	[Signal]
	public delegate void PlayerDiedEventHandler(int playerCount);

	// Pickup Signals
	[Signal]
	public delegate void PlayerPickupCollectedEventHandler(
		Vector2 collidedPlayerPosition
	);

	// Game State Signals
	[Signal]
	public delegate void GameStartedEventHandler();

	public static SignalBus Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
