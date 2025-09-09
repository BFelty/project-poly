using System;
using Godot;

namespace LastPolygon.Enemies;

public partial class EnemySpawner : Area2D
{
	private PackedScene _enemyScene = GD.Load<PackedScene>(
		"uid://ca1o6vvko4gbe"
	);

	private Vector2 PickRandomSpawnPoint()
	{
		Random r = new();
		float spawnY = (float)r.Next(-80, 80);

		Vector2 spawnPoint = new(0, spawnY);

		return spawnPoint;
	}

	public void SpawnEnemy()
	{
		Vector2 spawnPoint = PickRandomSpawnPoint();
		Enemy enemy = _enemyScene.Instantiate() as Enemy;
		enemy.Position = Position + spawnPoint;
		GetParent().AddChild(enemy);
	}
}
