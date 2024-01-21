using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SensoryWorlds.Objects;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        public struct CheckpointActivateEventArgs
        {
            public Checkpoint Checkpoint { get; }
            public bool Silent { get; }

            public CheckpointActivateEventArgs(Checkpoint checkpoint, bool silent)
            {
                Checkpoint = checkpoint;
                Silent = silent;
            }
        }
        
        [field: SerializeField] public Checkpoint ActiveCheckpoint { get; private set; }
        private List<Checkpoint> checkpoints = new();
        private int minCheckpointIndex, maxCheckpointIndex;

        public event EventHandler<CheckpointActivateEventArgs> ActivateCheckpoint;
        public event EventHandler<Checkpoint> DeactivateCheckpoint;

        private void Start()
        {
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint")
                .Select(c => c.GetComponent<Checkpoint>())
                .ToList();
            
            minCheckpointIndex = checkpoints.Min(checkpoint => checkpoint.Index);
            maxCheckpointIndex = checkpoints.Max(checkpoint => checkpoint.Index);

            StartCoroutine(InitializeCheckpoint());
        }

        private IEnumerator InitializeCheckpoint()
        {
            yield return new WaitForEndOfFrame();
            if (ActiveCheckpoint is not null)
                ActivateCheckpoint?.Invoke(this, new CheckpointActivateEventArgs(ActiveCheckpoint, true));
        }

        public void SetActiveCheckpoint(Checkpoint checkpoint)
        {
            if (checkpoint != ActiveCheckpoint)
            {
                DeactivateCheckpoint?.Invoke(this, ActiveCheckpoint);
                ActiveCheckpoint = checkpoint;
                ActivateCheckpoint?.Invoke(this, new CheckpointActivateEventArgs(ActiveCheckpoint, false));

                GameManager.Instance.Intensity =
                    Mathf.InverseLerp(minCheckpointIndex, maxCheckpointIndex, checkpoint.Index) *
                    GameManager.Instance.MaxIntensity;
            }
        }
    }
}