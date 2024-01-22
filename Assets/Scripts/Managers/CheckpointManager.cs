using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SensoryWorlds.Controllers;
using SensoryWorlds.Objects;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        public struct CheckpointActivateEventArgs
        {
            public PlayerController Player { get; }
            public Checkpoint Checkpoint { get; }
            public bool Silent { get; }

            public CheckpointActivateEventArgs(PlayerController player, Checkpoint checkpoint, bool silent)
            {
                Player = player;
                Checkpoint = checkpoint;
                Silent = silent;
            }
        }
        
        [field: SerializeField] public Checkpoint ActiveCheckpoint { get; private set; }
        private List<Checkpoint> checkpoints = new();
        private int minCheckpointIntensity, maxCheckpointIntensity;

        public event EventHandler<CheckpointActivateEventArgs> ActivateCheckpoint;
        public event EventHandler<Checkpoint> DeactivateCheckpoint;

        private void Start()
        {
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint")
                .Select(c => c.GetComponent<Checkpoint>())
                .ToList();
            
            minCheckpointIntensity = checkpoints.Min(checkpoint => checkpoint.Intensity);
            maxCheckpointIntensity = checkpoints.Max(checkpoint => checkpoint.Intensity);
            
            GameManager.Instance.StartGame += OnStartGame;
        }

        private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
        {
            InitializeCheckpoint(GameManager.Instance.InitialCheckpoint);
        }

        private void InitializeCheckpoint(Checkpoint checkpoint)
        {
            StartCoroutine(PerformInitializeCheckpoint(checkpoint));
        }

        private IEnumerator PerformInitializeCheckpoint(Checkpoint checkpoint)
        {
            yield return new WaitForEndOfFrame();
            if (checkpoint is not null)
                SetActiveCheckpoint(checkpoint, true);
        }

        public void SetActiveCheckpoint(Checkpoint checkpoint, bool silent = false)
        {
            if (checkpoint != ActiveCheckpoint)
            {
                DeactivateCheckpoint?.Invoke(this, ActiveCheckpoint);
                ActiveCheckpoint = checkpoint;
                ActivateCheckpoint?.Invoke(this, new CheckpointActivateEventArgs(ComponentCache.Instance.Player, ActiveCheckpoint, silent));

                GameManager.Instance.Intensity =
                    Mathf.InverseLerp(minCheckpointIntensity, maxCheckpointIntensity, checkpoint.Intensity) *
                    GameManager.Instance.MaxIntensity;
            }
        }
    }
}