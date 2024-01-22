using System;
using SensoryWorlds.Managers;
using SensoryWorlds.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace SensoryWorlds.UI
{
    public class HudController : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text TimeInfoText { get; private set; }
        [field: SerializeField] public TMP_Text GemsInfoText { get; private set; }
        [field: SerializeField] public TMP_Text DeathsInfoText { get; private set; }
        
        [field: SerializeField] public Animator GemsInfoAnimator { get; private set; }
        
        private Animator animator;
        private static readonly int ShowHudAnimation = Animator.StringToHash("Show");

        private void Start()
        {
            animator = GetComponent<Animator>();
            
            ScoreManager.Instance.TimeUpdate += OnTimeUpdate;
            ScoreManager.Instance.GemsUpdate += OnGemsUpdate;
            ScoreManager.Instance.DeathsUpdate += OnDeathsUpdate;
            
            GameManager.Instance.StartGame += OnStartGame;
            GameManager.Instance.StopGame += OnStopGame;
        }

        private void OnDestroy()
        {
            ScoreManager.Instance.TimeUpdate -= OnTimeUpdate;
            ScoreManager.Instance.GemsUpdate -= OnGemsUpdate;
            ScoreManager.Instance.DeathsUpdate -= OnDeathsUpdate;
            
            GameManager.Instance.StartGame -= OnStartGame;
            GameManager.Instance.StopGame -= OnStopGame;
        }

        private void OnStopGame(object sender, EventArgs e)
        {
            animator.SetBool(ShowHudAnimation, false);
        }

        private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
        {
            animator.SetBool(ShowHudAnimation, true);
        }

        private void OnDeathsUpdate(object sender, int deaths)
        {
            DeathsInfoText.text = $"{deaths}";
        }

        private void OnGemsUpdate(object sender, int gems)
        {
            GemsInfoText.text = $"{gems}";
            GemsInfoAnimator.Play("Popup");
        }

        private void OnTimeUpdate(object sender, float time)
        {
            TimeInfoText.text = TimeUtil.GetTimestamp(time);
        }
    }
}