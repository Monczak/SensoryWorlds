using UnityEngine;
using UnityEngine.InputSystem;

namespace SensoryWorlds.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public float ForceAmount { get; private set; }
        private float acceleration;

        private Rigidbody2D rb;
        
        private void Start()
        {
            Application.targetFrameRate = 90;
            
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!GravitySensor.current.enabled) InputSystem.EnableDevice(GravitySensor.current);

            var gravity = GravitySensor.current.gravity.ReadValue();
            acceleration = -Vector3.Cross(gravity, Vector3.down).z;
        }

        private void FixedUpdate()
        {
            rb.AddForce(Vector2.right * (acceleration * ForceAmount));
        }
    }
}


