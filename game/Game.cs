using Godot;
using LastPolygon.Enemies;
using LastPolygon.Globals;
using LastPolygon.Players;
using LastPolygon.UI;
using LastPolygon.Upgrades;

namespace LastPolygon.Game;

public partial class Game : Node
{
	// In-game menu scenes
	private PackedScene _pauseMenuScene = GD.Load<PackedScene>(
		"uid://n1y0r7o4fsq0"
	);
	private PackedScene _gameOverScene = GD.Load<PackedScene>(
		"uid://bekdrbn7k6a"
	);

	// In-game menu nodes
	private PauseMenu _pauseMenu;
	private GameOverMenu _gameOverMenu;

	private EnemySpawner _enemySpawner;
	private PickupSpawner _pickupSpawner;
	private PlayerManager _playerManager;
	private Vector2 _playerOrigin = new(85, 270);

	private Timer _enemySpawnerTimer;
	private Timer _pickupSpawnerTimer;

	public override void _Ready()
	{
		GD.Print("Game ready");

		// Connect to global signals
		SignalBus.Instance.PlayerDied += CheckIfGameOver;

		// Enable UI elements relevant to the game
		_pauseMenu = _pauseMenuScene.Instantiate() as PauseMenu;
		AddChild(_pauseMenu);
		_gameOverMenu = _gameOverScene.Instantiate() as GameOverMenu;
		AddChild(_gameOverMenu);

		// Find nodes that were placed in the editor
		_playerManager = FindChild("PlayerManager") as PlayerManager;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_pickupSpawner = FindChild("PickupSpawner") as PickupSpawner;

		_enemySpawnerTimer = FindChild("EnemySpawnTimer") as Timer;
		_pickupSpawnerTimer = FindChild("PickupTimer") as Timer;

		GameStart();
	}

	public override void _ExitTree()
	{
		// Disconnect from global signals to prevent disposed object errors
		SignalBus.Instance.PlayerDied -= CheckIfGameOver;
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
		_pauseMenu.ProcessMode = ProcessModeEnum.Always;
		_gameOverMenu.ProcessMode = ProcessModeEnum.Disabled;
		_gameOverMenu.Visible = false;

		_playerManager.SpawnPlayer(_playerOrigin);
		_enemySpawnerTimer.Start();
		_pickupSpawnerTimer.Start();
	}

	private void GameOver()
	{
		// Set which UI elements should be processed on game over
		_pauseMenu.ProcessMode = ProcessModeEnum.Disabled;
		_gameOverMenu.ProcessMode = ProcessModeEnum.Always;
		_gameOverMenu.Visible = true;
	}

	private void CheckIfGameOver(int playerCount)
	{
		if (playerCount <= 0)
			GameOver();
	}
}
