using System;
using SensoryWorlds.Managers;
using TMPro;
using UnityEngine;

namespace SensoryWorlds.UI
{
    public class HudController : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text TimeInfoText { get; private set; }
        [field: SerializeField] public TMP_Text GemsInfoText { get; private set; }
        [field: SerializeField] public TMP_Text DeathsInfoText { get; private set; }
        
        [field: SerializeField] public Animator GemsInfoAnimator { get; private set; }

        private void Start()
        {
            ScoreManager.Instance.TimeUpdate += OnTimeUpdate;
            ScoreManager.Instance.GemsUpdate += OnGemsUpdate;
            ScoreManager.Instance.DeathsUpdate += OnDeathsUpdate;
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
            var minutes = Mathf.FloorToInt(time / 60);
            var seconds = Mathf.FloorToInt(time % 60);
            var milliseconds = Mathf.FloorToInt(time % 1 * 1000);

            TimeInfoText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
        }
    }
}