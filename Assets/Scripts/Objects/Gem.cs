using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Gem : MonoBehaviour
    {
        private bool alive; 
        
        private Animator animator;
        private static readonly int CollectAnimationTrigger = Animator.StringToHash("Collect");
        
        [field: SerializeField] public AudioEvent CollectSound { get; private set; }

        private void Start()
        {
            animator = GetComponent<Animator>();
            alive = true;
            
            GameManager.Instance.StartGame += OnStartGame;
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            gameObject.SetActive(true);
            alive = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && alive)
                StartCoroutine(StartCollectAnimation());
        }

        private IEnumerator StartCollectAnimation()
        {
            alive = false;
            AudioManager.Instance.Play(CollectSound);
            ScoreManager.Instance.Gems += 1;
            animator.SetTrigger(CollectAnimationTrigger);
            yield return new WaitForSeconds(0.5f);
            
            gameObject.SetActive(false);
        } 
    }
}


