using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SensoryWorlds.UI
{
    public class StartMenuController : MonoBehaviour
    {
        [field: SerializeField] public Button StartGameButton { get; private set; }

        private Animator animator;
        private static readonly int HideAnimationTrigger = Animator.StringToHash("Hide");

        private void Start()
        {
            animator = GetComponent<Animator>();
            StartGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            animator.SetTrigger(HideAnimationTrigger);
            GameManager.Instance.StartGameSequence(false);
        }
    }
}

