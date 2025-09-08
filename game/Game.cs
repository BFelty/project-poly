using Godot;
using LastPolygon.Enemies;
using LastPolygon.Players;

namespace LastPolygon.Game;

public partial class Game : Node
{
	private Player _player;
	private EnemySpawner _enemySpawner;
	private Timer _enemySpawnTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game ready");
		_player = FindChild("Player") as Player;
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_enemySpawnTimer = FindChild("EnemySpawnTimer") as Timer;
	}

	public void OnEnemySpawnTimerTimeout()
	{
		_enemySpawner.SpawnEnemy();
	}
}
