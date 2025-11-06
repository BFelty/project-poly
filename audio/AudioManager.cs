using System;
using System.Collections.Generic;
using Godot;
using LastPolygon.Audio;

namespace LastPolygon.Globals;

// Audio manager node. Intended to be globally loaded as a 2D Scene. Utilizes
// Create2dAudioAtLocation() and CreateAudio() to handle the playback and
// culling of simultaneous sound effects.
//
// To properly use, define enum SoundEffect.SOUND_EFFECT_TYPE for each unique
// sound effect, create a Node2D scene for this AudioManager script, add those
// SoundEffect resources to this globally loaded script's sound_effects, and
// setup your individual SoundEffect resources. Then, use
// Create2dAudioAtLocation() and CreateAudio() to play those sound effects
// either at a specific location or globally.
//
// See https://github.com/Aarimous/AudioManager for more information.
//
// @tutorial: https://www.youtube.com/watch?v=Egf2jgET3nQ
public partial class AudioManager : Node2D
{
	[Export]
	private SoundEffect[] _soundEffects;

	private Dictionary<
		SoundEffect.SoundEffectType,
		SoundEffect
	> _soundEffectsDict = [];

	public static AudioManager Instance { get; private set; }

	public override void _Ready()
	{
		foreach (SoundEffect soundEffect in _soundEffects)
		{
			_soundEffectsDict[soundEffect.Type] = soundEffect;
		}

		Instance = this;
	}

	// Creates a sound effect at a specific location if the limit has not been
	// reached. Pass location for the global position of the audio effect and
	// type for the SoundEffect to be queued
	public void Create2dAudioAtLocation(
		Vector2 location,
		SoundEffect.SoundEffectType type
	)
	{
		if (_soundEffectsDict.ContainsKey(type))
		{
			SoundEffect soundEffect = _soundEffectsDict[type];
			if (soundEffect.HasOpenLimit())
			{
				soundEffect.ChangeAudioCount(1);
				AudioStreamPlayer2D new2dAudio = new();
				AddChild(new2dAudio);

				new2dAudio.Position = location;
				new2dAudio.Stream = soundEffect.Audio;
				new2dAudio.VolumeDb = soundEffect.Volume;
				new2dAudio.PitchScale = soundEffect.PitchScale;
				new2dAudio.PitchScale += new RandomNumberGenerator().RandfRange(
					-soundEffect.PitchRandomness,
					soundEffect.PitchRandomness
				);

				new2dAudio.Finished += soundEffect.OnAudioFinished;
				new2dAudio.Finished += new2dAudio.QueueFree;

				new2dAudio.Play();
			}
		}
	}

	// Creates a sound effect if the limit has not been reached. Pass type for
	// the SoundEffect to be queued
	public void CreateAudio(SoundEffect.SoundEffectType type)
	{
		if (_soundEffectsDict.ContainsKey(type))
		{
			SoundEffect soundEffect = _soundEffectsDict[type];
			if (soundEffect.HasOpenLimit())
			{
				soundEffect.ChangeAudioCount(1);
				AudioStreamPlayer2D newAudio = new();
				AddChild(newAudio);

				newAudio.Stream = soundEffect.Audio;
				newAudio.VolumeDb = soundEffect.Volume;
				newAudio.PitchScale = soundEffect.PitchScale;
				newAudio.PitchScale += new RandomNumberGenerator().RandfRange(
					-soundEffect.PitchRandomness,
					soundEffect.PitchRandomness
				);

				newAudio.Finished += soundEffect.OnAudioFinished;
				newAudio.Finished += newAudio.QueueFree;

				newAudio.Play();
			}
		}
	}
}
