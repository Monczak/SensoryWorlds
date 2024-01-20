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
            Debug.Log("dead lol");
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.KillPlayer();
            }
        }
    }
}


