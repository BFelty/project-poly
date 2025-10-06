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
	public int CurrentWave
	{
		get { return _currentWaveIndex + 1; }
		private set { _currentWaveIndex = value; }
	}

	// Once all waves are completed, always return the last wave
	// TODO - Probably move this somewhere else
	private EnemyWave _currentEnemyWave =>
		(_currentWaveIndex < _enemyWaves.Count)
			? _enemyWaves[_currentWaveIndex]
			: _enemyWaves[^1];

	private int _currentEnemySequenceIndex = 0;
	private EnemySequence _currentEnemySequence =>
		_currentEnemyWave.EnemySequences[_currentEnemySequenceIndex];

	private int _enemiesSpawned = 0; // From current enemy sequence

	// TODO   increment enemy (whether that be the amount, sequence, or wave)
	public (EnemyResource enemy, float spawnDelay) NextEnemyWithDelay()
	{
		EnemyResource enemy = _currentEnemySequence.Enemy;
		float spawnDelay = _currentEnemySequence.SpawnInterval;

		IncrementIndexes();

		return (enemy, spawnDelay);
	}

	private void IncrementIndexes()
	{
		_enemiesSpawned++;
		if (_enemiesSpawned >= _currentEnemySequence.AmountToSpawn)
		{
			_enemiesSpawned = 0;
			_currentEnemySequenceIndex++;
			if (
				_currentEnemySequenceIndex
				>= _currentEnemyWave.EnemySequences.Length
			)
			{
				_currentEnemySequenceIndex = 0;
				_currentWaveIndex++;
			}
		}
	}
}
