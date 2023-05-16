using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class HoleSetup : Singleton<HoleSetup>
    {
        [Header("Settings")]
        [Range(0, 20)][SerializeField] private int resolution = 10;
        [SerializeField] private LayerMask defualtCollectableLayers;
        [Header("Carver")]
        [SerializeField] private Hole holePrefab;
        [SerializeField] private Carver carver;
        [System.NonSerialized] private List<Hole> holes = new();
    

        void Awake()
        {
            carver.SetUp(resolution);
        }

        public Hole AddHole()
        {
            Hole newHole = MonoBehaviour.Instantiate(holePrefab, this.transform);
            newHole.OnTransformChanged = () => carver.UpdateCarves(holes);
            newHole.SideCollision = true;
            holes.Add(newHole);
            return newHole;
        }

        public void RemoveHole(Hole hole)
        {
            holes.Remove(hole);
            Destroy(hole.gameObject);
        }
    }
}