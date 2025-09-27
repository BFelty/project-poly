using System.Collections.Generic;
using Godot;
using LastPolygon.Enemies;

namespace LastPolygon.Game;

public partial class WaveManager : Node
{
	private static List<EnemyWave> _enemyWaves =
	[
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_1.tres"),
		GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_2.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_3.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_4.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_5.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_6.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_7.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_8.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_9.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_10.tres"),
		//GD.Load<EnemyWave>("res://game/levels/common/waves/waves/wave_endless.tres"),
	];
	private int _currentWaveIndex = 0;
	private int _currentEnemySequenceIndex = 0;

	// Once all waves are completed, always return the last wave
	// TODO - Probably move this somewhere else
	private EnemyWave _currentEnemyWave =>
		(_currentWaveIndex < _enemyWaves.Count)
			? _enemyWaves[_currentWaveIndex]
			: _enemyWaves[^1];
	private EnemySequence _currentEnemySequence =>
		_currentEnemyWave.EnemySequences[_currentEnemySequenceIndex];
	public Enemy Enemy { get; set; }
	private int _enemiesAlreadySpawned = 0;
	public float SpawnDelay { get; set; }
	public int CurrentWaveCount { get; private set; } = 1;

	// TODO - Return tuple with next enemy and it's spawn delay,
	// TODO   increment enemy (whether that be the amount, sequence, or wave)
}
