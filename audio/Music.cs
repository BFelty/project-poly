using Godot;

namespace LastPolygon.Audio;

[GlobalClass]
public partial class Music : Resource
{
	public enum MusicTrack { }

	// The unique music track in the enum MusicTrack to associate with
	// this effect. Each MusicTrack resource should have it's own unique enum
	// MusicTrack setting
	[Export]
	public MusicTrack Track { get; set; }

	// The AudioStream audio resource to play
	[Export]
	public AudioStream Audio { get; set; }

	// The volume of _musicTrack
	[Export(PropertyHint.Range, "-40, 20,")]
	public float Volume { get; set; } = 0;
}
