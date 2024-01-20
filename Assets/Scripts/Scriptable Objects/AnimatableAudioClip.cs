using System;
using UnityEngine;
using UnityEngine.Audio;

namespace SensoryWorlds.ScriptableObjects
{
    [Serializable]
    public class AnimatableAudioClip
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public AudioMixerGroup AudioMixerGroup { get; private set; }
        [field: SerializeField] public AnimationCurve VolumeCurve { get; private set; }
    }
}