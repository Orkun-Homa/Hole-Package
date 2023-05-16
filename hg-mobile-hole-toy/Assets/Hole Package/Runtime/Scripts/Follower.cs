using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] public Rigidbody rb;
        [SerializeField] public CustomAnimator customAnim;
        [SerializeField] public float speedMult = 1.0f;
        [System.NonSerialized] public Hole targetHole;
        [System.NonSerialized] public float speed;

        public void Follow(Hole hole, float speed)
        {
            this.targetHole = hole;
            this.speed = speed;
        }

        void FixedUpdate()
        {
            Vector3 holeCenter = targetHole.transform.position;
            Vector3 direction = (transform.position - holeCenter).normalized;
            Vector3 radius = targetHole.radius * direction * 0.5f + direction * 0.5f;
            Vector3 finalTarget = holeCenter + radius;
            Vector3 velocity = (finalTarget - transform.position) * speed;
            Debug.DrawLine(holeCenter, finalTarget);

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            
            customAnim.SetWeight(velocity.magnitude * speedMult);

            customAnim.transform.forward = Vector3.Lerp(customAnim.transform.forward, velocity.normalized, Time.fixedDeltaTime * 6.0f);
        }

    }
}
