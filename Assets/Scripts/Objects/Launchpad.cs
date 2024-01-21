using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Launchpad : MonoBehaviour
    {
        [field: SerializeField] public float LaunchStrength { get; private set; }
        
        [field: SerializeField] public LaunchpadLauncher Launcher { get; private set; }
        [field: SerializeField] public AudioEvent LaunchSound { get; private set; }
        [field: SerializeField] public float SoundCooldown { get; private set; } = 0.2f;

        private float lastLaunchTime;
        
        // Start is called before the first frame update
        private void Start()
        {
            Launcher.LaunchStrength = LaunchStrength;
            
            Launcher.Launch += OnLaunch;
        }

        private void OnLaunch(object sender, EventArgs e)
        {
            if (Time.time - lastLaunchTime > SoundCooldown)
            {
                AudioManager.Instance.Play(LaunchSound);
                lastLaunchTime = Time.time;
            }
        }
    }
}


