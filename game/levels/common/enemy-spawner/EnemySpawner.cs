using System;
using Godot;

namespace LastPolygon.Enemies;

public partial class EnemySpawner : Area2D
{
	private Vector2 PickRandomSpawnPoint()
	{
		Random r = new();
		float spawnY = r.Next(-65, 65);

		Vector2 spawnPoint = new(0, spawnY);

		return spawnPoint;
	}

	public void SpawnEnemy(PackedScene enemyScene)
	{
		// Create enemy from PackedScene and set it's EnemyResource. This
		// will later define the enemy's stats, visuals, and behavior
		Enemy enemy = enemyScene.Instantiate() as Enemy;

		Vector2 spawnPoint = PickRandomSpawnPoint();
		enemy.Position = Position + spawnPoint;

		GetParent().AddChild(enemy);
	}
}
