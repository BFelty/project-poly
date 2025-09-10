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
		// Connect to signals
		SignalBus.Instance.PlayerPickupCollected += OnPlayerPickupCollected;

		_playerSpawner = _playerSpawnerScene.Instantiate() as PlayerSpawner;
		AddChild(_playerSpawner);
	}

	// Spawns a new player and keeps track of it
	public void SpawnPlayer(Vector2 spawnPoint)
	{
		_playerSpawner.SpawnPlayer(spawnPoint);
		_playerCount++;
	}

	private void OnPlayerPickupCollected(Vector2 collidedPlayerPosition)
	{
		GD.Print("Player at " + collidedPlayerPosition + " collected pickup!");

		Vector2 offset = new(0, 5);
		CallDeferred(MethodName.SpawnPlayer, collidedPlayerPosition + offset);
	}
}
