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
	private EnemyResource _standardEnemyResource = GD.Load<EnemyResource>(
		"uid://nkjwo8at1k5"
	);
	private EnemyResource _fastEnemyResource = GD.Load<EnemyResource>(
		"uid://bi0ua2cyvm5de"
	);
	private EnemyResource _tankEnemyResource = GD.Load<EnemyResource>(
		"uid://dee0ubqwluivp"
	);
	private EnemyResource _squigglyEnemyResource = GD.Load<EnemyResource>(
		"uid://dvmgdo4hy8kva"
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
		enemy.EnemyData = _squigglyEnemyResource;

		Vector2 spawnPoint = PickRandomSpawnPoint();
		enemy.Position = Position + spawnPoint;

		GetParent().AddChild(enemy);
	}
}
