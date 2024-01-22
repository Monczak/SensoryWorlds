using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using SensoryWorlds.Controllers;
using SensoryWorlds.Objects;
using SensoryWorlds.ScriptableObjects;
using SensoryWorlds.UI;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public struct StartGameEventArgs
        {
            public bool IsReplay { get; }

            public StartGameEventArgs(bool isReplay)
            {
                IsReplay = isReplay;
            }
        }
        
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
        [field: SerializeField] public Transform Gems { get; private set; }

        public int GemCount => Gems.childCount;
        
        [field: Header("Sounds")]
        [field: SerializeField] public AudioEvent VictorySound { get; private set; }

        private CameraController cameraController;
        
        [field: Header("UI")]
        [field: SerializeField] public ResultsMenuController ResultsMenuController { get; private set; }

        public event EventHandler<StartGameEventArgs> StartGame;
        public event EventHandler StopGame;
        public event EventHandler ResetGame;
        
        // Start is called before the first frame update
        private void Start()
        {
            cameraController = ComponentCache.Instance.MainCamera;
            Application.targetFrameRate = 60;
            
            ResetTheGame();
            
            InitializeGame();
        }

        public void InitializeGame()
        {
            StartCoroutine(PerformInitializeGameSequence());
        }

        private IEnumerator PerformInitializeGameSequence()
        {
            yield return new WaitForSeconds(1.5f);
            FadeOverlay.StartUnfade();
        }

        public void StartTheGame(bool isReplay)
        {
            SpawnPlayer();
            StartGame?.Invoke(this, new StartGameEventArgs(isReplay));
        }

        public void StopTheGame()
        {
            StopGame?.Invoke(this, null);
        }

        public void ResetTheGame()
        {
            ResetGame?.Invoke(this, null);
        }

        public void Victory()
        {
            StopTheGame();
            StartCoroutine(PerformVictorySequence());
        }

        private IEnumerator PerformVictorySequence()
        {
            AudioManager.Instance.Play(VictorySound);
            yield return new WaitForSeconds(2.3f);
            ResultsMenuController.Show();
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

        public void StartGameSequence(bool isReplay)
        {
            StartCoroutine(PerformStartGameSequence(isReplay));
        }

        private IEnumerator PerformStartGameSequence(bool isReplay)
        {
            if (!isReplay)
                yield return new WaitForSeconds(0.7f);
            
            StopTheGame();

            if (isReplay)
            {
                FadeOverlay.StartFade();
                yield return new WaitForSeconds(1.2f);
            }
            
            ResetTheGame();
            StartTheGame(isReplay);

            if (isReplay)
            {
                FadeOverlay.StartUnfade();
            }
        }
    }
}

