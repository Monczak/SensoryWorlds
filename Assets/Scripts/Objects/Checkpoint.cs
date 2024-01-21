using System;
using SensoryWorlds.Controllers;
using SensoryWorlds.Managers;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Checkpoint : MonoBehaviour
    {
        private Animator animator;
        private static readonly int ActiveAnimationTrigger = Animator.StringToHash("Active");
        private static readonly int FromRightAnimationBool = Animator.StringToHash("From Right");
        
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public int Intensity { get; private set; }
        
        [field: SerializeField] public AudioEvent ActivateSound { get; private set; }

        private void Start()
        {
            CheckpointManager.Instance.ActivateCheckpoint += OnActivateCheckpoint;
            CheckpointManager.Instance.DeactivateCheckpoint += OnDeactivateCheckpoint;

            animator = GetComponent<Animator>();
        }

        private void OnDeactivateCheckpoint(object sender, Checkpoint e)
        {
            if (e != this) return;
            animator.SetBool(ActiveAnimationTrigger, false);
        }

        private void OnActivateCheckpoint(object sender, CheckpointManager.CheckpointActivateEventArgs e)
        {
            if (e.Checkpoint != this) return;

            if (!e.Silent)
            {
                animator.SetBool(FromRightAnimationBool, e.Player.transform.position.x > transform.position.x);
                animator.SetBool(ActiveAnimationTrigger, true);
                AudioManager.Instance.Play(ActivateSound);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                CheckpointManager.Instance.SetActiveCheckpoint(this);
        }
    }
}