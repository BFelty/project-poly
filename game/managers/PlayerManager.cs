using Godot;

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
		_playerSpawner = _playerSpawnerScene.Instantiate() as PlayerSpawner;
		AddChild(_playerSpawner);
	}

	// Spawns a new player and keeps track of it
	public void SpawnPlayer(Vector2 spawnPoint)
	{
		_playerSpawner.SpawnPlayer(spawnPoint);
		_playerCount++;
	}
}
