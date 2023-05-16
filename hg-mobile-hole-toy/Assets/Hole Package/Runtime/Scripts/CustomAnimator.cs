using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class CustomAnimator : MonoBehaviour
    {
        [SerializeField] public Animator anim;
        [SerializeField] public float speedMult = 1.0f;

        public void SetWeight(float weigth)
        {
            anim.SetFloat("Blend", weigth);
        }
    }
}
