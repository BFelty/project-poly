using Godot;
using LastPolygon.Enemies;
using LastPolygon.Players;
using LastPolygon.Upgrades;

namespace LastPolygon.Game;

public partial class Game : Node
{
	private PackedScene _playerPickup = GD.Load<PackedScene>(
		"uid://crxe8lhp1lr10"
	);

	private Player _player;
	private EnemySpawner _enemySpawner;
	private Marker2D _playerPickupSpawnPoint;

	private Timer _enemySpawnTimer;
	private Timer _playerPickupTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game ready");
		_player = FindChild("Player") as Player;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_playerPickupSpawnPoint =
			FindChild("PlayerPickupSpawnPoint") as Marker2D;

		_enemySpawnTimer = FindChild("EnemySpawnTimer") as Timer;
		_playerPickupTimer = FindChild("PlayerPickupTimer") as Timer;
	}

	public void OnEnemySpawnTimerTimeout()
	{
		_enemySpawner.SpawnEnemy();
	}

	public void OnPlayerPickupTimerTimeout()
	{
		// TODO - Move this to a dedicated PickupSpawner class
		Vector2 spawnPoint = _playerPickupSpawnPoint.GlobalPosition;
		PlayerPickup playerPickup = _playerPickup.Instantiate() as PlayerPickup;
		playerPickup.GlobalPosition = spawnPoint;
		GetParent().AddChild(playerPickup);
	}
}
