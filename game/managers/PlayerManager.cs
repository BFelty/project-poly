using System.Collections.Generic;
using Godot;
using LastPolygon.Globals;

namespace LastPolygon.Players;

public partial class PlayerManager : Node
{
	private PackedScene _playerSpawnerScene = GD.Load<PackedScene>(
		"uid://ib7e2mistls3"
	);
	private PlayerSpawner _playerSpawner;

	private List<Player> _playerList = new();
	private int _currentPlayerIndex = 0;
	private bool canShoot = true;

	public override void _Ready()
	{
		// Connect to global signals
		SignalBus.Instance.PlayerPickupCollected += OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged += KillPlayer;

		_playerSpawner = _playerSpawnerScene.Instantiate() as PlayerSpawner;
		AddChild(_playerSpawner);
	}

	public override void _ExitTree()
	{
		// Disconnect from global signals to prevent disposed object errors
		SignalBus.Instance.PlayerPickupCollected -= OnPlayerPickupCollected;
		SignalBus.Instance.PlayerDamaged -= KillPlayer;
	}

	public override void _PhysicsProcess(double delta)
	{
		HandleShooting();
	}

	public void SpawnPlayer(Vector2 spawnPoint)
	{
		Player newPlayer = _playerSpawner.SpawnPlayer(spawnPoint);
		_playerList.Add(newPlayer); // Track newly-spawned player
		GD.Print("Updated player count: " + _playerList.Count);
	}

	public void KillPlayer(Player playerToKill)
	{
		// Ensures a Player's death is not counted multiple times
		if (playerToKill.IsDead)
			return;

		playerToKill.IsDead = true;
		playerToKill.Kill();
		_playerList.Remove(playerToKill);
		GD.Print("Updated player count: " + _playerList.Count);

		// Signal that a player died, include _playerCount
		SignalBus.Instance.EmitSignal(
			SignalBus.SignalName.PlayerDied,
			_playerList.Count
		);
	}

	private void OnPlayerPickupCollected(Vector2 collidedPlayerPosition)
	{
		GD.Print("Player at " + collidedPlayerPosition + " collected pickup!");

		Vector2 offset = new(5, 0);
		CallDeferred(MethodName.SpawnPlayer, collidedPlayerPosition + offset);
	}

	// ! There is a bug in this function that occurs when many Players die
	// ! in quick succession and the player is shooting. _currentPlayerIndex
	// ! isn't updated quickly enough which results in the _currentPlayerIndex
	// ! pointing to a Player that no longer exists.
	// !
	// ! This bug could cause bullets to not be shot which could lower dps and
	// ! cause the player to lose. This issue probably won't happen as often
	// ! with a lower fire rate
	private void HandleShooting()
	{
		if (Input.IsActionPressed("shoot"))
		{
			if (canShoot && _playerList.Count > 0)
			{
				//canShoot = false;
				//_fireRateTimer.Start();

				// Make current player shoot
				// Increment the player that is allowed to shoot
				GD.Print("Current player index: " + _currentPlayerIndex);
				_playerList[_currentPlayerIndex].Shoot();
				_currentPlayerIndex =
					(_currentPlayerIndex + 1) % _playerList.Count;
			}
		}
		// TODO - Implement shared fire rate timer (wait time / player count)
	}
}
