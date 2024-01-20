using System;
using UnityEngine;

namespace SensoryWorlds.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            Instance ??= (T)this;
            if (Instance != this) Destroy(gameObject);
        }
    }
}