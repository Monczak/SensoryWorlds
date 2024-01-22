using System;
using System.Collections;
using SensoryWorlds.Managers;
using SensoryWorlds.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SensoryWorlds.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public bool Dead { get; private set; }
        [field: SerializeField] public float ForceAmount { get; private set; }
        [field: SerializeField] public float BrakingPower { get; private set; }
        [field: SerializeField] public ParticleSystem ExplodeParticles { get; private set; }
        [field: SerializeField] public ParticleSystem AppearParticles { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        [field: SerializeField] public AudioEvent DeathSound { get; private set; }
        [field: SerializeField] public AudioEvent FallSound { get; private set; }
        [field: SerializeField] public AudioEvent RespawnSound { get; private set; }
        
        private float acceleration;

        private Rigidbody2D rb;

        private Controls controls;
        private Vector3 gravityInput;
        private Animator animator;
        private static readonly int AppearAnimationTrigger = Animator.StringToHash("Appear");

        private void Start()
        {
            controls = new Controls();
            controls.PlayerControls.Gravity.performed += OnGravityPerformed;
            
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            controls.Enable();
            
            AppearParticles.Play();
            animator.SetTrigger(AppearAnimationTrigger);
            
            GameManager.Instance.StartGame += OnStartGame;
            GameManager.Instance.StopGame += OnStopGame;
        }

        private void OnDestroy()
        {
            GameManager.Instance.StartGame -= OnStartGame;
            GameManager.Instance.StopGame -= OnStopGame;
        }

        private void OnStartGame(object sender, GameManager.StartGameEventArgs e)
        {
            controls.Enable();
            rb.drag = 0;
        }

        private void OnStopGame(object sender, EventArgs e)
        {
            controls.Disable();
            gravityInput = Vector3.zero;
            rb.drag = 5;
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
            
            transform.position = pos;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }

        public void Kill(bool explode)
        {
            StartCoroutine(PerformKillSequence(explode));
        }

        private IEnumerator PerformKillSequence(bool explode)
        {
            Dead = true;
            if (explode)
            {
                SpriteRenderer.enabled = false;
                ExplodeParticles.Play();
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                AudioManager.Instance.Play(DeathSound);
            }
            else
            {
                AudioManager.Instance.Play(FallSound);
            }

            yield return new WaitForSeconds(0.6f);
            AudioManager.Instance.Play(RespawnSound);
            yield return new WaitForSeconds(0.6f);

            if (explode)
            {
                SpriteRenderer.enabled = true;
                rb.constraints = RigidbodyConstraints2D.None;
            }

            Dead = false;
        }
    }
}


