using System;
using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Objects
{
    public class KillArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.KillPlayer(false);
            }
        }
    }
}


