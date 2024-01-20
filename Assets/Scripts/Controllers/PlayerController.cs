using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SensoryWorlds.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public float ForceAmount { get; private set; }
        [field: SerializeField] public float BrakingPower { get; private set; }
        [field: SerializeField] public ParticleSystem ExplodeParticles { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
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
            if (GravitySensor.current is not null && !GravitySensor.current.enabled) InputSystem.EnableDevice(GravitySensor.current);
            
            acceleration = -Vector3.Cross(gravityInput, Vector3.down).z;
            
            var brakingAmount = Mathf.Max(acceleration * -rb.velocity.x * BrakingPower, 0) * Mathf.Sign(-acceleration);
            acceleration -= brakingAmount;
        }

        private void FixedUpdate()
        {
            rb.AddForce(Vector2.right * (acceleration * ForceAmount));
        }

        public void SpawnAt(Transform target)
        {
            Vector2 pos = target is null ? Vector2.zero : target.position;
            
            rb.MovePosition(pos);
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        public void PlayExplodeAnimation()
        {
            StartCoroutine(StartExplodeAnimation());
        }

        private IEnumerator StartExplodeAnimation()
        {
            SpriteRenderer.enabled = false;
            ExplodeParticles.Play();
            yield return new WaitForSeconds(1.2f);
            SpriteRenderer.enabled = true;
        }
    }
}


