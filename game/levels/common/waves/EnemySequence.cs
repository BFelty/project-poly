using Godot;

namespace LastPolygon.Enemies;

[GlobalClass]
public partial class EnemySequence : Resource
{
	[Export]
	public EnemyResource Enemy { get; set; }

	[Export]
	public int AmountToSpawn { get; set; }

	[Export]
	public float SpawnInterval { get; set; }

	public EnemySequence()
		: this(null, 0, 0f) { }

	public EnemySequence(EnemyResource enemy, int amount, float interval)
	{
		Enemy = enemy;
		AmountToSpawn = amount;
		SpawnInterval = interval;
	}
}
