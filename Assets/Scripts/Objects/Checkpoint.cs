using System;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Checkpoint : MonoBehaviour
    {
        private void Start()
        {
            CheckpointManager.Instance.ActivateCheckpoint += OnActivateCheckpoint;
            CheckpointManager.Instance.DeactivateCheckpoint += OnDeactivateCheckpoint;
        }

        private void OnDeactivateCheckpoint(object sender, Checkpoint e)
        {
            if (e != this) return;
        }

        private void OnActivateCheckpoint(object sender, Checkpoint e)
        {
            if (e != this) return;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                CheckpointManager.Instance.SetActiveCheckpoint(this);
        }
    }
}