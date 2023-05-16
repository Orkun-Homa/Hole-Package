using System.Collections;
using System.Collections.Generic;
using Toy.Hole;
using UnityEngine;

namespace Toy.Hole
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : Singleton<CameraController>
    {
        [System.NonSerialized] private Camera cam;
        [System.NonSerialized] private Transform camTransform;
        [SerializeField] public Transform followTarget;
        [SerializeField] public Vector3 offsetDistance;
        [SerializeField] public float catchUpSpeed = 8.0f;

        void Awake()
        {
            cam = GetComponent<Camera>();
            camTransform = cam.transform;
        }

        void FixedUpdate()
        {
            if (followTarget == null)
            {
                return;
            }
            camTransform.position = Vector3.Lerp(camTransform.position, followTarget.position + offsetDistance, Time.fixedDeltaTime * catchUpSpeed);    
        }
    }
}
