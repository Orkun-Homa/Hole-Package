using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Toy.Hole
{
    public class HoleDemo : MonoBehaviour
    {
        [SerializeField] private VariableJoystick joystick;
        [SerializeField] private float speed = 2.0f;
        [SerializeField] private ParticleSystem plusPS;
        [SerializeField] private Follower follower;
        [SerializeField] private float followerSpeed = 2.0f;
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private float enemySpeed = 1.0f;
        [SerializeField] private int enemyCount = 20;
        [System.NonSerialized] private Hole hole;

        void Awake()
        {
            Application.targetFrameRate = 60;    
        }
        void Start()
        {
            //Steady hole
            //Hole steadyHole = HoleSetup.Instance.AddHole();
            //steadyHole.collector.OnCollect = (go) =>
            //{
            //    go.SetActive(false);
            //    Debug.Log(go.name + " collected by steady hole");
            //};

            //steadyHole.transform.position = new Vector3(0.0f, 0.0f, 4.0f);
            //steadyHole.transform.localScale = Vector3.zero;
            //steadyHole.transform.DOScale(Vector3.one * 4.5f, 0.75f).SetEase(Ease.OutBack);


            //Controlled hole

            hole = HoleSetup.Instance.AddHole();
            hole.OnHoleTrigger = (other) => 
                {
                    if (other.gameObject.layer == 20)
                    {
                        Rigidbody rb = other.GetComponent<Rigidbody>();
                        rb.isKinematic = false;
                        rb.velocity = (transform.position - rb.position).normalized * 1.5f;
                    }
                    else if (other.gameObject.layer == 24)
                    {
                        Enemy enemy = other.GetComponent<Enemy>();
                        enemy.Kill(hole.transform);
                    }
                };
            hole.collector.OnCollect = (go) =>
            {
                go.SetActive(false);
                Debug.Log(go.name);

                hole.AddRadiusAnimated(0.015f, 0.25f, Ease.OutBack);

                plusPS.Emit(1);
            };

            hole.transform.position = new Vector3(0.0f, 0.0f, -3.0f);
            hole.transform.localScale = Vector3.zero;
            hole.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            plusPS.transform.parent = hole.transform;
            plusPS.transform.localPosition = Vector3.zero;
            plusPS.transform.localScale = Vector3.one;

            CameraController.Instance.followTarget = hole.transform;

            follower.Follow(hole, followerSpeed);

            SpawnEnemies(enemyCount);
        }

        private void SpawnEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Enemy enemy = MonoBehaviour.Instantiate(enemyPrefab, this.transform);
                enemy.transform.position = new Vector3(Random.Range(-20.0f, 20.0f), 0.0f, Random.Range(-20.0f, 20.0f));
                enemy.Attack(follower.transform, enemySpeed);
            }
        }

        void FixedUpdate()
        {
            hole.Move(speed * Time.fixedDeltaTime * new Vector3(joystick.Horizontal, 0.0f, joystick.Vertical));    
        }
    }
}