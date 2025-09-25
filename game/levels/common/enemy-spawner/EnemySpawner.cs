using System;
using Godot;
using LastPolygon.Resources;

namespace LastPolygon.Enemies;

public partial class EnemySpawner : Area2D
{
	private PackedScene _enemyScene = GD.Load<PackedScene>(
		"uid://ca1o6vvko4gbe"
	);

	// ! This will be a parameter for the SpawnEnemy function later, but for
	// ! now, I specify it here for testing
	private EnemyResource _enemyResource = GD.Load<EnemyResource>(
		"uid://nkjwo8at1k5"
	);

	private Vector2 PickRandomSpawnPoint()
	{
		Random r = new();
		float spawnY = (float)r.Next(-65, 65);

		Vector2 spawnPoint = new(0, spawnY);

		return spawnPoint;
	}

	public void SpawnEnemy()
	{
		// Create enemy from PackedScene and set it's EnemyResource. This
		// will later define the enemy's stats, visuals, and behavior
		Enemy enemy = _enemyScene.Instantiate() as Enemy;
		enemy.EnemyData = _enemyResource;

		Vector2 spawnPoint = PickRandomSpawnPoint();
		enemy.Position = Position + spawnPoint;

		GetParent().AddChild(enemy);
	}
}
