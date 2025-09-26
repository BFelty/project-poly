using Godot;

namespace LastPolygon.Components.Movement;

[GlobalClass]
public abstract partial class BaseMovementStrategy : Resource
{
	public abstract void Move(
		CharacterBody2D objectToMove,
		float speed,
		double delta
	);
}
