using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes for Enemy")]
    [SerializeField] private float speed = 1f;

    private int indexOfPath = 0;
    private Transform nextPoint;
    private float baseEnemySpeed;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }
    private void Start()
    {
        ResetEnemyPosition();
        baseEnemySpeed = speed;
    }
    public void ResetEnemyPosition()
    {
        indexOfPath = 0;
        nextPoint = LevelManager.main.pathPoints2[indexOfPath];
        _cachedTransform.position = nextPoint.position;
    }
    private void OnEnable()
    {
        ResetEnemyPosition();
    }

    private void Update()
    {
        if (Vector2.Distance(nextPoint.position, _cachedTransform.position) <= 0.1f)
        {
            indexOfPath++;

            if (indexOfPath == LevelManager.main.pathPoints2.Length)
            {
                EnemySpawner.onEnemyKilled.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                nextPoint = LevelManager.main.pathPoints2[indexOfPath];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (nextPoint.position - _cachedTransform.position).normalized;

        rb.velocity = direction * speed;
    }

    public void CurentSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void ResetEnemySpeed()
    {
        speed = baseEnemySpeed;
    }
}
