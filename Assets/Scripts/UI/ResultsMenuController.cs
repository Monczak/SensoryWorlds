using SensoryWorlds.Managers;
using SensoryWorlds.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SensoryWorlds.UI
{
    public class ResultsMenuController : MonoBehaviour
    {
        [field: SerializeField] public Button PlayAgainButton { get; private set; }
        
        [field: SerializeField] public TMP_Text TimeText { get; private set; }
        [field: SerializeField] public TMP_Text GemsText { get; private set; }
        [field: SerializeField] public TMP_Text DeathsText { get; private set; }

        private Animator animator;
        private static readonly int ShowAnimationTrigger = Animator.StringToHash("Show");

        // Start is called before the first frame update
        private void Start()
        {
            animator = GetComponent<Animator>();
            PlayAgainButton.onClick.AddListener(PlayAgain);
            GameManager.Instance.StartGame += OnStartGame;
        }

        private void OnDestroy()
        {
            PlayAgainButton.onClick.RemoveListener(PlayAgain);
            GameManager.Instance.StartGame -= OnStartGame;
        }

        private void PlayAgain()
        {
            GameManager.Instance.StartGameSequence(true);
        }

        private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
        {
            animator.Play("Hidden");
        }

        public void Show()
        {
            TimeText.text = TimeUtil.GetTimestamp(ScoreManager.Instance.ElapsedTime);
            GemsText.text = $"{ScoreManager.Instance.Gems} / {GameManager.Instance.GemCount}";
            DeathsText.text = $"{ScoreManager.Instance.Deaths}";
            animator.SetTrigger(ShowAnimationTrigger);
        }
    }
}


