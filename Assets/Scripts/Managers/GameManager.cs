using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using SensoryWorlds.Controllers;
using SensoryWorlds.Objects;
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
        
        [field: Header("Prefabs")]
        [field: SerializeField] public GameObject PlayerPrefab { get; private set; }
        
        [field: Header("Objects")]
        [field: SerializeField] public Checkpoint InitialCheckpoint { get; private set; }
        [field: SerializeField] public Transform Level { get; private set; }
        [field: SerializeField] public FadeOverlay FadeOverlay { get; private set; }

        private CameraController cameraController;

        public event EventHandler StartGame;
        public event EventHandler StopGame;
        public event EventHandler ResetGame;
        
        // Start is called before the first frame update
        private void Start()
        {
            cameraController = ComponentCache.Instance.MainCamera;
            Application.targetFrameRate = 60;
            
            ResetTheGame();
        }

        public void StartTheGame()
        {
            SpawnPlayer();
            StartGame?.Invoke(this, null);
        }

        public void StopTheGame()
        {
            StopGame?.Invoke(this, null);
        }

        public void ResetTheGame()
        {
            ResetGame?.Invoke(this, null);
        }

        public void SpawnPlayer()
        {
            if (ComponentCache.Instance.Player is not null)
            {
                Destroy(ComponentCache.Instance.Player.gameObject);
            }

            var player = Instantiate(PlayerPrefab, Level).GetComponent<PlayerController>();
            player.transform.position = InitialCheckpoint.transform.position;
            ComponentCache.Instance.Player = player;
        }
        
        public void KillPlayer(bool explodePlayer)
        {
            if (!ComponentCache.Instance.Player.Dead)
                StartCoroutine(PerformKillPlayerSequence(explodePlayer));
        }

        private IEnumerator PerformKillPlayerSequence(bool explodePlayer)
        {
            ComponentCache.Instance.Player.Kill(explodePlayer);
               
            FadeOverlay.StartFade();
            yield return new WaitForSeconds(1.2f);

            ScoreManager.Instance.Deaths += 1;
            
            ComponentCache.Instance.Player.SpawnAt(CheckpointManager.Instance.ActiveCheckpoint.transform);
            yield return new WaitForFixedUpdate();
            cameraController.CenterPosition();
            FadeOverlay.StartUnfade();
        }
    }
}

