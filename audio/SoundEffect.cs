using System;
using Godot;

namespace LastPolygon.Audio;

// Sound effect resource, used to configure unique sound effects for use with
// the AudioManager. Passed to AudioManager.Create2dAudioAtLocation() and
// AudioManager.CreateAudio() to play sound effects
[GlobalClass]
public partial class SoundEffect : Resource
{
	// Stores the different types of sounds effects available to be played to
	// distinguish them from one another. Each new SoundEffect resource
	// created should add to this enum, to allow them to be easily instantiated
	// via AudioManager.Create2dAudioAtLocation() and
	// AudioManager.CreateAudio()
	public enum SoundEffectType
	{
		Gunshot,
		GunshotImpact,
		PlayerDeath,
		WaveStart,
		ZombieDeath,
	}

	// Maximum number of this SoundEffect to play simultaneously before culled
	[Export(PropertyHint.Range, "0, 20,")]
	private int _limit = 5;

	// The unique sound effect in the enum SoundEffectType to associate with
	// this effect. Each SoundEffect resource should have it's own unique enum
	// SoundEffectType setting
	[Export]
	public SoundEffectType Type;

	// The AudioStreamMP3 audio resource to play
	[Export]
	public AudioStream Audio;

	// The volume of _soundEffect
	[Export(PropertyHint.Range, "-40, 20,")]
	public float Volume = 0;

	// The pitch scale of _soundEffect
	[Export(PropertyHint.Range, "0.0, 4.0, 0.01")]
	public float PitchScale = 1.0f;

	// The pitch randomness setting of _soundEffect
	[Export(PropertyHint.Range, "0.0, 1.0, 0.01")]
	public float PitchRandomness = 0.0f;

	// The instances of this AudioStream currently playing
	public int _audioCount = 0;

	public void ChangeAudioCount(int amount)
	{
		_audioCount = Math.Max(0, _audioCount + amount);
	}

	// Checks whether the audio limit is reached
	public bool HasOpenLimit()
	{
		return _audioCount < _limit;
	}

	public void OnAudioFinished()
	{
		ChangeAudioCount(-1);
	}
}
