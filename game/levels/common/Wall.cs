using Godot;
using LastPolygon.Globals;
using LastPolygon.Players;

namespace LastPolygon.Game;

public partial class Wall : Area2D
{
	public void OnBodyEntered(Node2D body)
	{
		if (body is Player)
		{
			// Emit signal from SignalBus autoload
			SignalBus.Instance.EmitSignal(
				SignalBus.SignalName.PlayerDamaged,
				body as Player
			);
		}
	}
}
