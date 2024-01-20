using System;
using System.Collections.Generic;
using SensoryWorlds.ScriptableObjects;
using SensoryWorlds.Utils;
using UnityEngine;

namespace SensoryWorlds.Managers
{
    public class AudioManager : Singleton<AudioManager>
    {
        [field: Header("Parents")]
        [field: SerializeField] public GameObject AudioParent { get; private set; }
        [field: SerializeField] public GameObject BackgroundMusicParent { get; private set; }
        
        [field: Header("Prefabs")]
        [field: SerializeField] public GameObject BackgroundMusicTrackPrefab { get; private set; }
        [field: SerializeField] public GameObject AudioEventPrefab { get; private set; }

        private Dictionary<AudioSource, AnimatableAudioClip> backgroundMusicAudioSources;
        
        [field: Header("BGM")]
        [field: SerializeField] public BackgroundMusic BackgroundMusic { get; set; }
        [field: SerializeField] public float BackgroundMusicIntensity { get; set; }
        [field: SerializeField] public float BackgroundMusicIntensitySmoothing { get; set; }

        private float actualBackgroundMusicIntensity;

        private void Start()
        {
            PlayBackgroundMusic();
        }

        private void Update()
        {
            actualBackgroundMusicIntensity = Mathf.SmoothStep(actualBackgroundMusicIntensity, BackgroundMusicIntensity,
                BackgroundMusicIntensitySmoothing * Time.deltaTime);
            
            foreach (var (source, track) in backgroundMusicAudioSources) 
                source.volume = track.VolumeCurve.Evaluate(actualBackgroundMusicIntensity);
        }

        private void PlayBackgroundMusic()
        {
            backgroundMusicAudioSources = new Dictionary<AudioSource, AnimatableAudioClip>();
            foreach (GameObject child in BackgroundMusicParent.transform)
                Destroy(child);

            foreach (var track in BackgroundMusic.Tracks)
            {
                var source = Instantiate(BackgroundMusicTrackPrefab).GetComponent<AudioSource>();
                source.gameObject.transform.parent = BackgroundMusicParent.transform;

                source.clip = track.AudioClip;
                source.outputAudioMixerGroup = track.AudioMixerGroup;
                source.loop = true;
                source.Play();
                
                backgroundMusicAudioSources.Add(source, track);
            }
        }

        private void Play(AudioClip audioClip)
        {
            
        }
    }
}