using System;
using Godot;
using LastPolygon.Players;

namespace LastPolygon.Globals;

public partial class EventBus : Node
{
	// Player Events
	public static event Action<Player> PlayerDamaged;

	public static void InvokePlayerDamaged(Player player) =>
		PlayerDamaged?.Invoke(player);

	public static event Action<int> PlayerCountChanged;

	public static void InvokePlayerCountChanged(int playerCount) =>
		PlayerCountChanged?.Invoke(playerCount);

	// Enemy Events
	public static event Action EnemyLeak;

	public static void InvokeEnemyLeak() => EnemyLeak?.Invoke();

	// Pickup Events
	public static event Action<Vector2> PlayerPickupCollected;

	public static void InvokePlayerPickupCollected(
		Vector2 collidedPlayerPosition
	) => PlayerPickupCollected?.Invoke(collidedPlayerPosition);

	// Game Events
	public static event Action<int> WaveCompleted;

	public static void InvokeWaveCompleted(int currentWave) =>
		WaveCompleted?.Invoke(currentWave);

	public static event Action<float> GameEnded;

	public static void InvokeGameEnded(float timeSurvived) =>
		GameEnded?.Invoke(timeSurvived);
}
