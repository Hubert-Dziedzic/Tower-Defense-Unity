using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif



public class SlowTurret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes of Turret")]
    [SerializeField] private float bulletsPerSec = 1.7f;
    [SerializeField] private float turretRange = 4f;
    [SerializeField] private float slowmotionSpeed = 0.5f;
    [SerializeField] private float slowmotionTimie = 1.3f;

    private float timeUntilAttack;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;

    }
    private void Update()
    {

            timeUntilAttack += Time.deltaTime;
            if (timeUntilAttack >= 1f / bulletsPerSec)
            {
                FreezeInRange();
                timeUntilAttack = 0f;
            }
    }

    private void FreezeInRange()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_cachedTransform.position, turretRange, 
            (Vector2)_cachedTransform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i< hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovment em = hit.transform.GetComponent<EnemyMovment>();
                em.CurentSpeed(slowmotionSpeed);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovment em)
    {
        yield return new WaitForSeconds(slowmotionTimie);

        em.ResetEnemySpeed();
    }
    private void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
        #endif
    }
}

