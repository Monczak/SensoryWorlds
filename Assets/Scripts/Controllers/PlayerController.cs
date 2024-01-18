using UnityEngine;
using UnityEngine.InputSystem;

namespace SensoryWorlds.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public float ForceAmount { get; private set; }
        private float acceleration;

        private Rigidbody2D rb;

        private Controls controls;
        private Vector3 gravityInput;
        
        private void Start()
        {
            controls = new Controls();
            controls.PlayerControls.Gravity.performed += OnGravityPerformed;
            
            rb = GetComponent<Rigidbody2D>();
            
            controls.Enable();
        }

        private void OnGravityPerformed(InputAction.CallbackContext obj)
        {
            gravityInput = obj.ReadValue<Vector3>();
        }

        private void Update()
        {
            if (!GravitySensor.current.enabled) InputSystem.EnableDevice(GravitySensor.current);
            
            acceleration = -Vector3.Cross(gravityInput, Vector3.down).z;
        }

        private void FixedUpdate()
        {
            rb.AddForce(Vector2.right * (acceleration * ForceAmount));
        }
    }
}


