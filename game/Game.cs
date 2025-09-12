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

		// Connect to global signals
		SignalBus.Instance.PlayerDied += CheckIfGameOver;
		SignalBus.Instance.GameStarted += GameStart;

		// Enable UI elements relevant to the game
		AddChild(UIManager.Instance.PauseMenu);
		AddChild(UIManager.Instance.GameOverMenu);

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

	public void GameStart()
	{
		// Set which UI elements should be processed during the game
		UIManager.Instance.PauseMenu.ProcessMode = ProcessModeEnum.Always;
		UIManager.Instance.GameOverMenu.ProcessMode = ProcessModeEnum.Disabled;
		UIManager.Instance.GameOverMenu.Visible = false;

		_playerManager.SpawnPlayer(_playerOrigin);
		_enemySpawnerTimer.Start();
		_pickupSpawnerTimer.Start();
	}

	private void GameOver()
	{
		// Set which UI elements should be processed on game over
		UIManager.Instance.PauseMenu.ProcessMode = ProcessModeEnum.Disabled;
		UIManager.Instance.GameOverMenu.ProcessMode = ProcessModeEnum.Always;
		UIManager.Instance.GameOverMenu.Visible = true;
	}

	private void CheckIfGameOver(int playerCount)
	{
		if (playerCount <= 0)
			GameOver();
	}
}
