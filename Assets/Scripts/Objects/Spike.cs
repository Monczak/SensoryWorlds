using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class Spike : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                GameManager.Instance.KillPlayer(true);
        }
    }
}

