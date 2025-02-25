using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributtes of Bullet")]
    [SerializeField] private float projectileVelocity = 5f;
    [SerializeField] private int projectileDmg = 1;
    [SerializeField] private float lifeTime = 3f;


    private Transform targetEnemy;
    private Coroutine _returnToPoolTimerCoroutine;

    private Transform _cachedTransform;

    private void Awake()
    {
        StartCoroutine(DestroyAfterTime());
        _cachedTransform = transform;
    }
    private void FixedUpdate()
    {
        if (!targetEnemy) return;
        Vector2 direction = (targetEnemy.position - _cachedTransform.position).normalized;

        rb.velocity = direction * projectileVelocity;                               

        RotateProjectile(direction);
    }

    public void RotateProjectile(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public void SetTarget(Transform _targetEnemy)
    {
        targetEnemy = _targetEnemy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        collision.gameObject.GetComponent<HP>().TakeDamage(projectileDmg);
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTime);
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }
    private void OnEnable()
    {
        _returnToPoolTimerCoroutine = StartCoroutine(ReturnPoolAfterTime());
    }
    private IEnumerator ReturnPoolAfterTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < lifeTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }
}