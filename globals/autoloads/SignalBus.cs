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

	// Enemy Signals
	[Signal]
	public delegate void EnemyLeakEventHandler();

	// Pickup Signals
	[Signal]
	public delegate void PlayerPickupCollectedEventHandler(
		Vector2 collidedPlayerPosition
	);

	// Game Signals
	[Signal]
	public delegate void GameEndedEventHandler(float timeSurvived);

	public static SignalBus Instance { get; private set; }

	public override void _Ready()
	{
		Instance = this;
	}
}
