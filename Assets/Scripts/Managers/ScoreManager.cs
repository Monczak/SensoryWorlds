using System;
using SensoryWorlds.Utils;
using UnityEngine;

namespace SensoryWorlds.Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private float elapsedTime;
        [SerializeField] private int gems;
        [SerializeField] private int deaths;

        private bool isRunning = false;

        public float ElapsedTime
        {
            get => elapsedTime;
            private set
            {
                elapsedTime = value;
                TimeUpdate?.Invoke(this, elapsedTime);
            } 
        }
        
        public int Gems
        {
            get => gems;
            set
            {
                gems = value;
                GemsUpdate?.Invoke(this, gems);
            }
        }
        
        public int Deaths
        {
            get => deaths;
            set
            {
                deaths = value;
                DeathsUpdate?.Invoke(this, deaths);
            }
        }

        public event EventHandler<float> TimeUpdate;
        public event EventHandler<int> GemsUpdate;
        public event EventHandler<int> DeathsUpdate;

        private void Start()
        {
            GameManager.Instance.StartGame += OnStartGame;
            GameManager.Instance.StopGame += OnStopGame;
            GameManager.Instance.ResetGame += OnResetGame;
        }

        private void OnStopGame(object sender, EventArgs e)
        {
            isRunning = false;
        }

        private void OnStartGame(object sender, EventArgs e)
        {
            isRunning = true;
        }

        private void OnResetGame(object sender, EventArgs e)
        {
            ResetTimer();
            Gems = 0;
            Deaths = 0;
        }

        public void ResetTimer()
        {
            ElapsedTime = 0;
        }

        private void Update()
        {
            if (isRunning)
                ElapsedTime += Time.deltaTime;
        }
    }
}