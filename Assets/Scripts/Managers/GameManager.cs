using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using UnityEngine;

namespace SensoryWorlds.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        public CameraController MainCamera { get; private set; }

        private void Awake()
        {
            if (Instance is null) Instance = this;
            if (Instance != this) Destroy(gameObject);
        }

        // Start is called before the first frame update
        void Start()
        {
            Application.targetFrameRate = 60;
            
            MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

