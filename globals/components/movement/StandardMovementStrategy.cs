using Godot;

namespace LastPolygon.Components.Movement;

// Enemy moves straight to the left at a consistent speed
// This requires the GlobalClass attribute to be used within Godot's editor
[GlobalClass]
public partial class StandardMovementStrategy : BaseMovementStrategy
{
	public override void Move(
		CharacterBody2D objectToMove,
		float speed,
		double delta
	)
	{
		objectToMove.Velocity = Vector2.Left * speed;
		objectToMove.MoveAndSlide();
	}
}
