using Godot;
using LastPolygon.Enemies;
using LastPolygon.Players;
using LastPolygon.Upgrades;

namespace LastPolygon.Game;

public partial class Game : Node
{
	private Player _player;
	private EnemySpawner _enemySpawner;
	private PickupSpawner _pickupSpawner;

	private Timer _enemySpawnTimer;
	private Timer _pickupTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game ready");
		_player = FindChild("Player") as Player;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_pickupSpawner = FindChild("PickupSpawner") as PickupSpawner;

		_enemySpawnTimer = FindChild("EnemySpawnTimer") as Timer;
		_pickupTimer = FindChild("PickupTimer") as Timer;
	}

	public void OnEnemySpawnTimerTimeout()
	{
		_enemySpawner.SpawnEnemy();
	}

	public void OnPlayerPickupTimerTimeout()
	{
		_pickupSpawner.SpawnPlayerPickup();
	}
}
