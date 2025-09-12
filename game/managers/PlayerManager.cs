using Godot;
using LastPolygon.Globals;

namespace LastPolygon.Players;

public partial class PlayerManager : Node
{
	private PackedScene _playerSpawnerScene = GD.Load<PackedScene>(
		"uid://ib7e2mistls3"
	);
	private PlayerSpawner _playerSpawner;
	private int _playerCount = 0;

	public override void _Ready()
	{
		// Connect to global signals
		SignalBus.Instance.PlayerPickupCollected += OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged += KillPlayer;

		_playerSpawner = _playerSpawnerScene.Instantiate() as PlayerSpawner;
		AddChild(_playerSpawner);
	}

	public override void _ExitTree()
	{
		// Disconnect from global signals to prevent disposed object errors
		SignalBus.Instance.PlayerPickupCollected -= OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged -= KillPlayer;
	}

	public void SpawnPlayer(Vector2 spawnPoint)
	{
		_playerSpawner.SpawnPlayer(spawnPoint);
		_playerCount++;
		GD.Print("Updated player count: " + _playerCount);
	}

	public void KillPlayer(Player playerToKill)
	{
		// Ensures a Player's death is not counted multiple times
		if (playerToKill.IsDead)
			return;

		playerToKill.IsDead = true;
		playerToKill.Kill();
		_playerCount--;
		GD.Print("Updated player count: " + _playerCount);

		// Signal that a player died, include _playerCount
		SignalBus.Instance.EmitSignal(
			SignalBus.SignalName.PlayerDied,
			_playerCount
		);
	}

	private void OnPlayerPickupCollected(Vector2 collidedPlayerPosition)
	{
		GD.Print("Player at " + collidedPlayerPosition + " collected pickup!");

		Vector2 offset = new(5, 0);
		CallDeferred(MethodName.SpawnPlayer, collidedPlayerPosition + offset);
	}
}
