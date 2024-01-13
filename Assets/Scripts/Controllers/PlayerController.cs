using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

namespace SensoryWorlds.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public float ForceAmount { get; private set; }
        private float acceleration;

        private Rigidbody2D rb;
        
        private void Awake()
        {
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


