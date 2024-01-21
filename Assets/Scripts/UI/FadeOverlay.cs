using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using SensoryWorlds.Controllers;
using SensoryWorlds.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SensoryWorlds.UI
{
    [ExecuteInEditMode]
    public class FadeOverlay : MonoBehaviour
    {
        private Material material;
        [SerializeField] private PlayerController player;
        [SerializeField] private UnityEngine.Camera mainCamera;

        [SerializeField] private Animator animator;

        [field: SerializeField] public Vector2 HolePosition { get; private set; }
        [field: SerializeField] public float HoleSize { get; private set; } = 1;
        [field: SerializeField] public float HoleRotation { get; private set; } = 0;
        
        private static readonly int HolePositionProp = Shader.PropertyToID("_Hole_Position");
        private static readonly int HoleSizeProp = Shader.PropertyToID("_Hole_Size");
        private static readonly int HoleRotationProp = Shader.PropertyToID("_Hole_Rotation");
        private static readonly int FadeAnimationTrigger = Animator.StringToHash("Fade");
        private static readonly int UnfadeAnimationTrigger = Animator.StringToHash("Unfade");

        // Start is called before the first frame update
        private void Start()
        {
            material = GetComponent<Image>().material;
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            var topLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
            var bottomRight = mainCamera.ViewportToWorldPoint(Vector3.one);
            var screenDiagonal = (topLeft - bottomRight).magnitude;
            
            material.SetFloat(HoleSizeProp, HoleSize * screenDiagonal);
            material.SetFloat(HoleRotationProp, HoleRotation);

            if (Application.isPlaying)
                HolePosition = player.transform.position;
            material.SetVector(HolePositionProp, -HolePosition);
        }

        public void StartFade()
        {
            animator.SetTrigger(FadeAnimationTrigger);
        }
        
        public void StartUnfade()
        {
            animator.SetTrigger(UnfadeAnimationTrigger);
        }
    }
}


