using System;
using SensoryWorlds.Controllers;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Checkpoint : MonoBehaviour
    {
        private PlayerController playerController;
        
        private Animator animator;
        private static readonly int ActiveAnimationTrigger = Animator.StringToHash("Active");
        private static readonly int FromRightAnimationBool = Animator.StringToHash("From Right");

        private void Start()
        {
            CheckpointManager.Instance.ActivateCheckpoint += OnActivateCheckpoint;
            CheckpointManager.Instance.DeactivateCheckpoint += OnDeactivateCheckpoint;

            animator = GetComponent<Animator>();

            playerController = ComponentCache.Instance.Player;
        }

        private void OnDeactivateCheckpoint(object sender, Checkpoint e)
        {
            if (e != this) return;
            animator.SetBool(ActiveAnimationTrigger, false);
        }

        private void OnActivateCheckpoint(object sender, Checkpoint e)
        {
            if (e != this) return;
            animator.SetBool(FromRightAnimationBool, playerController.transform.position.x > transform.position.x);
            animator.SetBool(ActiveAnimationTrigger, true);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                CheckpointManager.Instance.SetActiveCheckpoint(this);
        }
    }
}