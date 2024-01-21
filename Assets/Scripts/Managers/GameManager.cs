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
        [Header("Gameplay")]
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
        
        [field: SerializeField] public float MaxIntensity { get; private set; }
        
        [field: Header("Objects")]
        [field: SerializeField] public FadeOverlay FadeOverlay { get; private set; }

        private PlayerController player;
        private CameraController cameraController;
        
        // Start is called before the first frame update
        private void Start()
        {
            player = ComponentCache.Instance.Player;
            cameraController = ComponentCache.Instance.MainCamera;
            Application.targetFrameRate = 60;
            
            ScoreManager.Instance.ResetTimer();
        }

        public void KillPlayer(bool explodePlayer)
        {
            if (!player.Dead)
                StartCoroutine(PerformKillPlayerSequence(explodePlayer));
        }

        private IEnumerator PerformKillPlayerSequence(bool explodePlayer)
        {
            player.Kill(explodePlayer);
               
            FadeOverlay.StartFade();
            yield return new WaitForSeconds(1.2f);

            ScoreManager.Instance.Deaths += 1;
            
            player.SpawnAt(CheckpointManager.Instance.ActiveCheckpoint.transform);
            yield return new WaitForFixedUpdate();
            cameraController.CenterPosition();
            FadeOverlay.StartUnfade();
        }
    }
}

