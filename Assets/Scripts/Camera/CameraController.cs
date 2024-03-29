using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public Transform Target
        {
            get => target;
            set
            {
                target = value;
                targetRigidbody = target.GetComponent<Rigidbody2D>();
            }
        }
        private Rigidbody2D targetRigidbody;
        
        [field: SerializeField] public float SmoothTime { get; private set; }
        [field: SerializeField] public float LookaheadFactor { get; private set; }
        [field: SerializeField] public float MaxLookahead { get; private set; }

        private Vector2 currentVelocity;
        private float initialZ;

        private Vector2 targetVelocity;
        private Vector2 targetPosition;

        public UnityEngine.Camera Camera { get; private set; }
        
        private void Start()
        {
            Camera = GetComponent<UnityEngine.Camera>();
            initialZ = transform.position.z;
            
            GameManager.Instance.StartGame += OnStartGame;
        }

        private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
        {
            Target = ComponentCache.Instance.Player.transform;
            
            if (e.IsReplay)
                CenterPosition();
        }

        private void LateUpdate()
        {
            TrackTarget();
        }

        private void TrackTarget()
        {
            var screenRatio = new Vector2(1, (float)Screen.height / Screen.width);

            if (Target != null)
            {
                targetPosition = Target.transform.position;
                if (targetRigidbody != null)
                    targetVelocity = targetRigidbody.velocity;
                else
                    targetVelocity = Vector2.zero;
            }
            else
            {
                targetVelocity = Vector2.zero;
            }
            
            var lookahead = targetVelocity * LookaheadFactor * screenRatio;
            targetPosition += Vector2.ClampMagnitude(lookahead, MaxLookahead);
            
            var smoothedPosition = Vector2.SmoothDamp(transform.position,
                new Vector2(targetPosition.x, targetPosition.y), ref currentVelocity,
                SmoothTime);

            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, initialZ);
        }

        public void CenterPosition()
        {
            if (Target is not null)
            {
                transform.position = Target.position;
                currentVelocity = Vector2.zero;
            }
        }
    }   
}

