using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;

public class VictoryCup : MonoBehaviour
{
    private Animator animator;
    private static readonly int VictoryAnimationTrigger = Animator.StringToHash("Victory");
    
    [field: SerializeField] public Transform SpriteTransform { get; private set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        GameManager.Instance.StartGame += OnStartGame;
    }
    
    private void OnDestroy()
    {
        GameManager.Instance.StartGame -= OnStartGame;
    }


    private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
    {
        animator.Play("Idle");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger(VictoryAnimationTrigger);
            ComponentCache.Instance.MainCamera.Target = SpriteTransform;
            GameManager.Instance.Victory();
        }
    }
}
