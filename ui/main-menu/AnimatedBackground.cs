using Godot;
using LastPolygon.Enemies;
using LastPolygon.Game;

public partial class AnimatedBackground : Node2D
{
	private WaveManager _waveManager = new();
	private EnemySpawner _enemySpawner;
	private Timer _enemySpawnerTimer;

	public override void _Ready()
	{
		_enemySpawner = GetNode<EnemySpawner>("EnemySpawner");
		_enemySpawnerTimer = GetNode<Timer>("EnemySpawnTimer");
	}

	private void OnEnemySpawnTimerTimeout()
	{
		var (enemyScene, spawnDelay) = _waveManager.NextEnemyWithDelay();
		_enemySpawner.SpawnEnemy(enemyScene);
		_enemySpawnerTimer.WaitTime = spawnDelay;
		_enemySpawnerTimer.Start(); // Restart the timer with proper wait time
	}
}
