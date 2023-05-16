using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public Rigidbody rb;
        [System.NonSerialized] public float speed = 3.0f;
        [System.NonSerialized] public Transform target;
        [System.NonSerialized] public bool killed = false;

        public void Attack(Transform target, float speed)
        {
            this.target = target;
            this.speed = speed;
        }

        void FixedUpdate()
        {
            Vector3 velocity = (target.position - transform.position).normalized * speed;
            Debug.DrawLine(target.position, transform.position, Color.red);
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        public void Kill(Transform parent)
        {
            if (killed)
            {
                return;
            }
            killed = true;

            GameObject go = new GameObject();
            go.transform.parent = parent;
            go.transform.localPosition = Vector3.zero;

            transform.parent = go.transform;

            float duration = Random.Range(0.5f, 0.75f);
            go.transform.DORotate(Vector3.up * Random.Range(140.0f, 200.0f), duration, RotateMode.FastBeyond360).SetEase(Ease.InSine).SetUpdate(UpdateType.Fixed)
                .onComplete += () => 
                { 
                    transform.parent = null;
                    Destroy(go);

                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.constraints = RigidbodyConstraints.None;
                    rb.angularVelocity = Random.insideUnitSphere * 4.0f;
                    rb.velocity = (parent.position - rb.position).normalized * 6.5f;
                };
            go.transform.DOScale(Vector3.one * 0.85f, duration).SetEase(Ease.InSine).SetUpdate(UpdateType.Fixed);
            go.transform.DOLocalMove(new Vector3(0.0f, Random.Range(0.5f, 1.0f), Random.Range(-0.25f, 0.25f)), duration).SetEase(Ease.OutBack).SetUpdate(UpdateType.Fixed);

            //rb.isKinematic = false;
            //rb.useGravity = true;
            //rb.constraints = RigidbodyConstraints.None;
            this.enabled = false;

            rb.isKinematic = true;


        }
    }
}
