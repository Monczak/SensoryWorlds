using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Launchpad : MonoBehaviour
    {
        [field: SerializeField] public float LaunchStrength { get; private set; }
        
        [field: SerializeField] public LaunchpadLauncher Launcher { get; private set; }
        
        // Start is called before the first frame update
        void Start()
        {
            Launcher.LaunchStrength = LaunchStrength;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}


