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

        public void ResetTimer()
        {
            ElapsedTime = 0;
        }

        private void Update()
        {
            ElapsedTime += Time.deltaTime;
        }
    }
}