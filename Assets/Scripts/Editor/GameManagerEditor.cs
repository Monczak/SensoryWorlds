using System;
using SensoryWorlds.Managers;
using UnityEditor;
using UnityEngine;

namespace SensoryWorlds.Editor
{
    [CustomEditor(typeof(GameManager)), CanEditMultipleObjects]
    public class GameManagerEditor : UnityEditor.Editor
    {
        private GameManager gameManager;

        private void Awake()
        {
            gameManager = target as GameManager;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Start") && Application.isPlaying)
                gameManager.StartTheGame(false);
            
            if (GUILayout.Button("Stop") && Application.isPlaying)
                gameManager.StopTheGame();
            
            if (GUILayout.Button("Reset") && Application.isPlaying)
                gameManager.ResetTheGame();
           
            if (GUILayout.Button("Start Sequence (Not Replay)") && Application.isPlaying)
                gameManager.StartGameSequence(false);
            
            if (GUILayout.Button("Start Sequence (Replay)") && Application.isPlaying)
                gameManager.StartGameSequence(true);
        }
    }
}