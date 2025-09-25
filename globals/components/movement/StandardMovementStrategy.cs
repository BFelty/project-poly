using Godot;

namespace LastPolygon.Components.Movement;

// Enemy moves straight to the left at a consistent speed
[GlobalClass]
public partial class StandardMovementStrategy : MovementStrategyBase
{
	public override void Move(
		CollisionObject2D objectToMove,
		float speed,
		double delta
	)
	{
		Vector2 velocity = Vector2.Left * speed;
		objectToMove.Translate(velocity * (float)delta);
	}
}
