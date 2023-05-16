using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toy.Hole
{
    public class Carver : MonoBehaviour
    {
        [SerializeField] private PolygonCollider2D polygonCollider2DHole;
        [SerializeField] private PolygonCollider2D polygonCollider2DGround;
        [SerializeField] private MeshCollider meshCollider;
        [System.NonSerialized] private Mesh mesh;

        public void SetUp(int resolution)
        {
            polygonCollider2DHole.points = GenerateCircularPoints(resolution);
        }

        private Vector2[] GenerateCircularPoints(int count)
        {
            float angle = 360.0f / count;
            Vector2[] points = new Vector2[count];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = Quaternion.Euler(0.0f, 0.0f, angle * i) * Vector2.up;
            }
            return points;
        }

        public void UpdateCarves(List<Hole> holes)
        {
            polygonCollider2DGround.pathCount = holes.Count + 1;

            for (int i = 0; i < holes.Count; i++)
            {
                polygonCollider2DHole.transform.position = new Vector2(holes[i].transform.position.x, holes[i].transform.position.z);
                polygonCollider2DHole.transform.localScale = holes[i].transform.localScale * 0.5f;

                CarveHole(i);
            }
            ConvertToMeshCollider();
        }
        private void CarveHole(int index)
        {
            Vector2[] holePoints = polygonCollider2DHole.GetPath(0);

            for (int i = 0; i < holePoints.Length; i++)
            {
                holePoints[i] = polygonCollider2DHole.transform.TransformPoint(holePoints[i]);
            }

            polygonCollider2DGround.SetPath(index + 1, holePoints);
        }

        private void ConvertToMeshCollider()
        {
            if (mesh != null)
            {
                Destroy(mesh);
            }

            mesh = polygonCollider2DGround.CreateMesh(true, true);
            meshCollider.sharedMesh = mesh;
        }
    }
}