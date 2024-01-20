using System;
using System.Collections.Generic;
using System.Linq;
using SensoryWorlds.Objects;
using UnityEngine;
using SensoryWorlds.Utils;

namespace SensoryWorlds.Managers
{
    public class CheckpointManager : Singleton<CheckpointManager>
    {
        [field: SerializeField] public Checkpoint ActiveCheckpoint { get; private set; }
        private List<Checkpoint> checkpoints = new();
        private int minCheckpointIndex, maxCheckpointIndex;

        public event EventHandler<Checkpoint> ActivateCheckpoint;
        public event EventHandler<Checkpoint> DeactivateCheckpoint;

        private void Start()
        {
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint")
                .Select(c => c.GetComponent<Checkpoint>())
                .ToList();
            
            minCheckpointIndex = checkpoints.Min(checkpoint => checkpoint.Index);
            maxCheckpointIndex = checkpoints.Max(checkpoint => checkpoint.Index);
        }

        public void SetActiveCheckpoint(Checkpoint checkpoint)
        {
            if (checkpoint != ActiveCheckpoint)
            {
                DeactivateCheckpoint?.Invoke(this, ActiveCheckpoint);
                ActiveCheckpoint = checkpoint;
                ActivateCheckpoint?.Invoke(this, ActiveCheckpoint);

                GameManager.Instance.Intensity =
                    Mathf.InverseLerp(minCheckpointIndex, maxCheckpointIndex, checkpoint.Index) *
                    GameManager.Instance.MaxIntensity;
            }
        }
    }
}