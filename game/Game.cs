using Godot;
using LastPolygon.Audio;
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
	private PackedScene _hudScene = GD.Load<PackedScene>("uid://dxpl2vo2ejouf");

	// In-game menu nodes
	private PauseMenu _pauseMenu;
	private GameOverMenu _gameOverMenu;
	private Hud _hud;

	// Managers / Spawners
	private WaveManager _waveManager = new();
	private EnemySpawner _enemySpawner;
	private PickupSpawner _pickupSpawner;
	private PlayerManager _playerManager;
	private Vector2 _playerOrigin = new(85, 270);

	// Timers
	private Timer _enemySpawnerTimer;
	private Timer _pickupSpawnerTimer;

	// Scoring variables
	private Timer _survivalTimer;
	private int _timeSurvivedInSeconds = 0;

	public override void _Ready()
	{
		GD.Print("Game ready");

		// Enable UI elements relevant to the game
		_pauseMenu = _pauseMenuScene.Instantiate() as PauseMenu;
		AddChild(_pauseMenu);
		_gameOverMenu = _gameOverScene.Instantiate() as GameOverMenu;
		AddChild(_gameOverMenu);
		_hud = _hudScene.Instantiate() as Hud;
		AddChild(_hud);

		// Find nodes that were placed in the editor
		_playerManager = FindChild("PlayerManager") as PlayerManager;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_pickupSpawner = FindChild("PickupSpawner") as PickupSpawner;

		_enemySpawnerTimer = FindChild("EnemySpawnTimer") as Timer;
		_pickupSpawnerTimer = FindChild("PickupTimer") as Timer;
		_survivalTimer = FindChild("SurvivalTimer") as Timer;

		GameStart();
	}

	public override void _ExitTree()
	{
		// Disconnect from global events to prevent disposed object errors
		DisconnectEvents();
	}

	private void ConnectEvents()
	{
		EventBus.PlayerCountChanged += CheckIfGameOver;
		EventBus.EnemyLeak += HandleEnemyLeak;
		EventBus.WaveCompleted += OnWaveComplete;
	}

	private void DisconnectEvents()
	{
		EventBus.PlayerCountChanged -= CheckIfGameOver;
		EventBus.EnemyLeak -= HandleEnemyLeak;
		EventBus.WaveCompleted -= OnWaveComplete;
	}

	// Timer timeouts
	public void OnEnemySpawnTimerTimeout()
	{
		var (enemyScene, spawnDelay) = _waveManager.NextEnemyWithDelay();
		_enemySpawner.SpawnEnemy(enemyScene);
		_enemySpawnerTimer.WaitTime = spawnDelay;
		_enemySpawnerTimer.Start(); // Restart the timer with proper wait time
	}

	public void OnPlayerPickupTimerTimeout()
	{
		_pickupSpawner.SpawnPlayerPickup();
	}

	private void OnSurvivalTimerTimeout()
	{
		_timeSurvivedInSeconds++;
	}

	public void GameStart()
	{
		// Connect to events when game starts
		ConnectEvents();

		// Set which UI elements should be processed during the game
		_pauseMenu.ProcessMode = ProcessModeEnum.Always;
		_gameOverMenu.ProcessMode = ProcessModeEnum.Disabled;
		_gameOverMenu.Visible = false;

		_playerManager.SpawnPlayer(_playerOrigin);
		_enemySpawnerTimer.Start();
		_pickupSpawnerTimer.Start();
		_survivalTimer.Start();

		AudioManager.Instance.ChangeMusic(Music.MusicTrack.ZombieMoans);
		AudioManager.Instance.CreateAudio(
			SoundEffect.SoundEffectType.WaveStart
		);
	}

	private void GameOver()
	{
		// Disconnect from events on game over to prevent continued handling
		DisconnectEvents();

		// Pause timer to get accurate milliseconds
		_survivalTimer.Paused = true;

		GD.Print("Game emit event: GameEnded");
		EventBus.InvokeGameEnded(
			_waveManager.CurrentWave,
			GetScoreWithMilliseconds()
		);

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

	private float GetScoreWithMilliseconds()
	{
		// Milliseconds formatted to 3 decimal places
		float milliseconds = $"{1 - _survivalTimer.TimeLeft:0.000}".ToFloat();
		return _timeSurvivedInSeconds + milliseconds;
	}

	private void HandleEnemyLeak()
	{
		GD.Print("Handling enemy leak...");
		GameOver();
	}

	private void OnWaveComplete(int _)
	{
		AudioManager.Instance.CreateAudio(
			SoundEffect.SoundEffectType.WaveStart
		);
	}
}
