using Godot;

namespace LastPolygon.Components.Movement;

[GlobalClass]
public abstract partial class BaseMovementStrategy : Resource
{
	public abstract void Move(
		CollisionObject2D objectToMove,
		float speed,
		double delta
	);
}
