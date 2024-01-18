using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensoryWorlds.Camera
{
    public class CameraController : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody2D Target { get; private set; }
        
        [field: SerializeField] public float SmoothTime { get; private set; }
        [field: SerializeField] public float LookaheadFactor { get; private set; }
        [field: SerializeField] public float MaxLookahead { get; private set; }

        private Vector3 currentVelocity;
        private float initialZ;
        
        private void Start()
        {
            initialZ = transform.position.z;
        }
        
        private void LateUpdate()
        {
            TrackTarget();
        }

        private void TrackTarget()
        {
            var screenRatio = new Vector2(1, (float)Screen.height / Screen.width);
            var lookahead = Target.velocity * LookaheadFactor * screenRatio;
            
            var targetPosition = (Vector2)Target.transform.position + Vector2.ClampMagnitude(lookahead, MaxLookahead);
            
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(targetPosition.x, targetPosition.y, initialZ), ref currentVelocity,
                SmoothTime);
        }
    }   
}

