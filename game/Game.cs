using Godot;
using LastPolygon.Enemies;
using LastPolygon.Players;

namespace LastPolygon.Game;

public partial class Game : Node
{
	private EnemySpawner _enemySpawner;
	private Player _player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game ready");
		_enemySpawner = FindChild("EnemySpawner") as EnemySpawner;
		_player = FindChild("Player") as Player;

		_enemySpawner.SpawnEnemy();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }
}
