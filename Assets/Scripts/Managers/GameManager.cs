using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using SensoryWorlds.Controllers;
using SensoryWorlds.UI;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private float intensity;
        public float Intensity
        {
            get => intensity;
            set
            {
                intensity = value;
                AudioManager.Instance.BackgroundMusicIntensity = intensity;
            }
        }
        
        [field: Header("Gameplay")]
        [field: SerializeField] public float MaxIntensity { get; private set; }
        
        [field: Header("Objects")]
        [field: SerializeField] public DeathOverlay DeathOverlay { get; private set; }

        private PlayerController player;
        private CameraController cameraController;
        
        // Start is called before the first frame update
        private void Start()
        {
            player = ComponentCache.Instance.Player;
            cameraController = ComponentCache.Instance.MainCamera;
            Application.targetFrameRate = 60;
        }

        public void KillPlayer(bool explodePlayer)
        {
            if (!player.Dead)
                StartCoroutine(PerformKillPlayerSequence(explodePlayer));
        }

        private IEnumerator PerformKillPlayerSequence(bool explodePlayer)
        {
            player.Kill(explodePlayer);
               
            DeathOverlay.StartDeathAnimation();
            yield return new WaitForSeconds(1.2f);
            
            player.SpawnAt(CheckpointManager.Instance.ActiveCheckpoint.transform);
            yield return new WaitForFixedUpdate();
            cameraController.CenterPosition();
            DeathOverlay.StartRespawnAnimation();
        }
    }
}

