using System;
using SensoryWorlds.Camera;
using SensoryWorlds.Controllers;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    [ExecuteInEditMode]
    public class ComponentCache : Singleton<ComponentCache>
    {
        public CameraController MainCamera { get; private set; }
        public PlayerController Player { get; private set; }

        private void Start()
        {
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }
}