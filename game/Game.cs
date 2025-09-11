using Godot;
using LastPolygon.Enemies;
using LastPolygon.Globals;
using LastPolygon.Players;
using LastPolygon.Upgrades;

namespace LastPolygon.Game;

public partial class Game : Node
{
	private EnemySpawner _enemySpawner;
	private PickupSpawner _pickupSpawner;
	private PlayerManager _playerManager;
	private Vector2 _playerOrigin = new(85, 270);

	private Timer _enemySpawnerTimer;
	private Timer _pickupSpawnerTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game ready");

		// Add the PauseMenu as a child of UIManager
		UIManager.Instance.AddChild(UIManager.Instance.PauseMenu);

		_playerManager = FindChild("PlayerManager") as PlayerManager;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_pickupSpawner = FindChild("PickupSpawner") as PickupSpawner;

		_enemySpawnerTimer = FindChild("EnemySpawnTimer") as Timer;
		_pickupSpawnerTimer = FindChild("PickupTimer") as Timer;

		GameStart();
	}

	public void OnEnemySpawnTimerTimeout()
	{
		_enemySpawner.SpawnEnemy();
	}

	public void OnPlayerPickupTimerTimeout()
	{
		_pickupSpawner.SpawnPlayerPickup();
	}

	private void GameStart()
	{
		_playerManager.SpawnPlayer(_playerOrigin);
		_enemySpawnerTimer.Start();
		_pickupSpawnerTimer.Start();
	}
}
