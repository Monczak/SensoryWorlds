using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensoryWorlds.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Audio Event")]
    public class AudioEvent : ScriptableObject
    {
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float Volume { get; private set; } = 1;
    }
}

