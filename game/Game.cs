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
	public PauseMenu PauseMenu { get; private set; }
	public GameOverMenu GameOverMenu { get; private set; }

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
		SignalBus.Instance.GameStarted += GameStart;

		// Enable UI elements relevant to the game
		PauseMenu = _pauseMenuScene.Instantiate() as PauseMenu;
		AddChild(PauseMenu);
		GameOverMenu = _gameOverScene.Instantiate() as GameOverMenu;
		AddChild(GameOverMenu);

		// Find nodes that were placed in the editor
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
		PauseMenu.ProcessMode = ProcessModeEnum.Always;
		GameOverMenu.ProcessMode = ProcessModeEnum.Disabled;
		GameOverMenu.Visible = false;

		_playerManager.SpawnPlayer(_playerOrigin);
		_enemySpawnerTimer.Start();
		_pickupSpawnerTimer.Start();
	}

	private void GameOver()
	{
		// Set which UI elements should be processed on game over
		PauseMenu.ProcessMode = ProcessModeEnum.Disabled;
		GameOverMenu.ProcessMode = ProcessModeEnum.Always;
		GameOverMenu.Visible = true;
	}

	private void CheckIfGameOver(int playerCount)
	{
		if (playerCount <= 0)
			GameOver();
	}
}
