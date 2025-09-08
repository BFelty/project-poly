using Godot;

namespace LastPolygon.Upgrades;

public partial class PickupSpawner : Marker2D
{
	private PackedScene _playerPickup = GD.Load<PackedScene>(
		"uid://crxe8lhp1lr10"
	);

	public void SpawnPlayerPickup()
	{
		Vector2 spawnPoint = GlobalPosition;
		PlayerPickup playerPickup = _playerPickup.Instantiate() as PlayerPickup;
		playerPickup.GlobalPosition = spawnPoint;
		GetParent().AddChild(playerPickup);
	}
}
