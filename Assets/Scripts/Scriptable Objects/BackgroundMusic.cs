using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensoryWorlds.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Background Music")]
    public class BackgroundMusic : ScriptableObject
    {
        [field: SerializeField]
        public List<AnimatableAudioClip> Tracks { get; private set; }
    }
}


