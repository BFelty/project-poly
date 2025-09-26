using Godot;

namespace LastPolygon.Enemies;

[GlobalClass]
public partial class Wave : Resource
{
	[Export]
	public EnemySequence[] EnemySequences { get; set; }
}
