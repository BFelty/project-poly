using Godot;

namespace LastPolygon.Components.Movement;

public abstract partial class MovementStrategyBase : Resource
{
	public abstract void Move(
		CollisionObject2D objectToMove,
		float speed,
		double delta
	);
}
