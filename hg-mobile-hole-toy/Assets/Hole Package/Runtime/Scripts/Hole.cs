using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private MeshCollider sideCollider;
        [SerializeField] private LayerMask collectableLayers;
        [SerializeField] private Rigidbody rb;
        [SerializeField] public Collector collector;
        [System.NonSerialized] public System.Action OnTransformChanged;
        [System.NonSerialized] public System.Action<Collider> OnHoleTrigger;
        [System.NonSerialized] public float radius = 1.0f;

        public bool SideCollision
        {
            set
            {
                sideCollider.enabled = value;
            }
            get { return sideCollider.enabled; }
        }

        public Hole SetCollectableLayer(LayerMask layerMask)
        {
            this.collectableLayers = layerMask;
            return this;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collectableLayers == (collectableLayers | (1 << other.gameObject.layer)))
            {
                OnHoleTrigger?.Invoke(other);
            }
        }

        public void Move(Vector3 velocity)
        {
            rb.MovePosition(rb.position + velocity);
        }
        void FixedUpdate()
        {
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                OnTransformChanged?.Invoke();
            }    
        }
    
        public void SetRadius(float amount)
        {
            this.transform.localScale = Vector3.one;
        }
        public void SetRadiusAnimated(float amount, float duration, Ease ease)
        {
            this.radius = amount;
            this.transform.DOKill();
            this.transform.DOScale(Vector3.one * radius, duration).SetEase(ease);
        }
        public void AddRadiusAnimated(float amount, float duration, Ease ease)
        {
            this.radius += amount;
            this.transform.DOKill();
            this.transform.DOScale(Vector3.one * radius, duration).SetEase(ease);
        }
    }
}