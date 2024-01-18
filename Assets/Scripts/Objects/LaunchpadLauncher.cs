using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class LaunchpadLauncher : MonoBehaviour
    {
        public float LaunchStrength { get; set; }
        
        private Rigidbody2D body;
        
        // Start is called before the first frame update
        void Start()
        {
            body = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                body.AddRelativeForce(Vector2.up * LaunchStrength, ForceMode2D.Impulse);
            }
        }
    }
}

