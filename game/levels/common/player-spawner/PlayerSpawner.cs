using System;
using Godot;
using LastPolygon.Globals;

namespace LastPolygon.Players;

public partial class PlayerSpawner : Node2D
{
	private PackedScene _playerScene = GD.Load<PackedScene>(
		"uid://osokqpha3208"
	);

	private CollisionShape2D collisionShape2D;

	public override void _Ready()
	{
		collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	private Vector2 PickRandomSpawnPoint()
	{
		Rect2I rect = (Rect2I)collisionShape2D.Shape.GetRect();
		int minRange = (int)Math.Floor(rect.Position.Y * Scale.Y + 30);
		int maxRange = (int)Math.Floor(rect.End.Y * Scale.Y);

		Random r = new();
		float spawnY = r.Next(minRange, maxRange);

		Vector2 spawnPoint = collisionShape2D.GlobalPosition;
		Vector2 spawnOffset = new(0, spawnY);

		return spawnPoint + spawnOffset;
	}

	public void SpawnUncontrolledPlayer()
	{
		Player player = _playerScene.Instantiate<Player>();

		Vector2 spawnPoint = PickRandomSpawnPoint();
		player.Position = spawnPoint;
		player.Scale = new Vector2(-player.Scale.X, player.Scale.Y);

		GetParent().AddChild(player);
	}

	public void SpawnControlledPlayerAtPoint(Vector2 spawnPoint)
	{
		Player player = _playerScene.Instantiate<Player>();
		player.IsControlled = true;
		player.GlobalPosition = spawnPoint;
		player.Scale = new Vector2(-player.Scale.X, player.Scale.Y);

		GetParent().AddChild(player);
		EventBus.InvokePlayerRecruited(player);
	}
}
