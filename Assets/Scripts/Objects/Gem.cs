using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Gem : MonoBehaviour
    {
        private Animator animator;
        private static readonly int CollectAnimationTrigger = Animator.StringToHash("Collect");

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                StartCoroutine(StartCollectAnimation());
        }

        private IEnumerator StartCollectAnimation()
        {
            ScoreManager.Instance.Gems += 1;
            animator.SetTrigger(CollectAnimationTrigger);
            yield return new WaitForSeconds(0.5f);
            Destroy(gameObject);
        } 
    }
}


