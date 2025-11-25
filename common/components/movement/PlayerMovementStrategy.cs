using System;
using Godot;
using LastPolygon.Players;

namespace LastPolygon.Components.Movement;

public partial class PlayerMovementStrategy : BaseMovementStrategy
{
	private Vector2 _target;
	private float _minimumDistanceToMove = 10f;
	private float _movementEpsilonSquared = 1f;

	public override void Move(
		CharacterBody2D objectToMove,
		float speed,
		double delta
	)
	{
		Player player = objectToMove as Player;
		_target = player.GetGlobalMousePosition();
		player.Velocity = player.GlobalPosition.DirectionTo(_target) * speed;

		if (player.GlobalPosition.DistanceTo(_target) < _minimumDistanceToMove)
		{
			player.HasMoved = false;
		}
		else
		{
			player.MoveAndSlide();

			// Check if the player actually moved
			player.HasMoved =
				player.GetPositionDelta().LengthSquared()
				> _movementEpsilonSquared;
		}

		// Keep the player from exiting the viewport
		player.Position = player.Position.Clamp(
			Vector2.Zero,
			player.GetViewportRect().Size
		);
	}
}
