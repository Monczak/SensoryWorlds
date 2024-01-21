using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;

namespace SensoryWorlds.Audio
{
    public class OneshotAudioSource : MonoBehaviour
    {
        [field: SerializeField] public AudioEvent AudioEvent { get; set; }
        private AudioSource audioSource;
        
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = AudioEvent.AudioClip;
            audioSource.volume = AudioEvent.Volume;
            audioSource.Play();
            StartCoroutine(ScheduleDestroy());
        }

        private IEnumerator ScheduleDestroy()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => !audioSource.isPlaying);
            Destroy(gameObject);
        }
    }
}


