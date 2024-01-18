using System.Collections;
using System.Collections.Generic;
using SensoryWorlds.Camera;
using SensoryWorlds.Managers;
using UnityEngine;

namespace SensoryWorlds.Background
{
    public class ParallaxBackground : MonoBehaviour
    {
        [field: SerializeField] public float ParallaxFactor { get; set; } = 1;
    
        private CameraController cameraController;
    
        // Start is called before the first frame update
        void Start()
        {
            cameraController = GameManager.Instance.MainCamera;
        }

        // Update is called once per frame
        void Update()
        {
            var pos = cameraController.transform.position / -(cameraController.transform.position.z - transform.position.z + 1) * ParallaxFactor;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }    
}
