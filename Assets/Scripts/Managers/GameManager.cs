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

        public void KillPlayer()
        {
            StartCoroutine(PerformKillPlayerSequence());
        }

        private IEnumerator PerformKillPlayerSequence()
        {
            DeathOverlay.StartDeathAnimation();
            yield return new WaitForSeconds(1.2f);
            
            player.SpawnAt(CheckpointManager.Instance.ActiveCheckpoint.transform);
            yield return new WaitForFixedUpdate();
            cameraController.CenterPosition();
            DeathOverlay.StartRespawnAnimation();
        }
    }
}

