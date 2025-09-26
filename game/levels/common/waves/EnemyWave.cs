using Godot;

namespace LastPolygon.Enemies;

[GlobalClass]
public partial class EnemyWave : Resource
{
	[Export]
	public EnemySequence[] EnemySequences { get; set; }
}
