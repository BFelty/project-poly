using Godot;

namespace LastPolygon.Enemies;

[GlobalClass]
public partial class EnemySequence : Resource
{
	[Export]
	public PackedScene EnemyScene { get; set; }

	[Export]
	public int AmountToSpawn { get; set; }

	[Export]
	public float SpawnInterval { get; set; }

	public EnemySequence()
		: this(null, 0, 0f) { }

	public EnemySequence(PackedScene enemyScene, int amount, float interval)
	{
		EnemyScene = enemyScene;
		AmountToSpawn = amount;
		SpawnInterval = interval;
	}
}
