using System;
using Godot;

namespace LastPolygon.Enemies;

public partial class EnemySpawner : Area2D
{
	private CollisionShape2D collisionShape2D;

	public override void _Ready()
	{
		collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	private Vector2 PickRandomSpawnPoint()
	{
		Rect2I rect = (Rect2I)collisionShape2D.Shape.GetRect();
		int minRange = (int)Math.Floor(rect.Position.Y * Scale.Y + 15);
		int maxRange = (int)Math.Floor(rect.End.Y * Scale.Y - 15);

		Random r = new();
		float spawnY = r.Next(minRange, maxRange);

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
