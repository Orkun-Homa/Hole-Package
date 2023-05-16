using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class Collector : MonoBehaviour
    {
        [System.NonSerialized] public System.Action<GameObject> OnCollect;

        private void OnTriggerEnter(Collider other)
        {
            OnCollect?.Invoke(other.gameObject);
        }
    }
}
