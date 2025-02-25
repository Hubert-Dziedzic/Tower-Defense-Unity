using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
#if UNITY_EDITOR
using TMPro.EditorUtilities;
#endif

public class Turret : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject UpgradeUI;
    [SerializeField] private Transform damageSpawnPoint;
    [SerializeField] private Button upgradeButton;

    [Header("Attributes of Turret")]
    [SerializeField] private float bulletsPerSec = 1f;
    [SerializeField] private float speedOfRotation = 7f;
    [SerializeField] private float turretRange = 5f;
    [SerializeField] private int upgradeCost = 100;
    [SerializeField] private float upgradeCostFactor = 0.8f;
    [SerializeField] private float upgradeBpsTowerFactor = 0.6f;
    [SerializeField] private float upgradeRangeTowerFactor = 0.4f;

    private float baseBps;
    private float baseturretRange;
    private float timeUntilAttack;
    private Transform targetEnemy;
    private int buildingLvl = 1;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;

    }

    private void Start()
    {
        upgradeButton.onClick.AddListener(UpgradeBuilding);
        baseBps = bulletsPerSec;
        baseturretRange = turretRange;
    }
    public void UpgradeBuilding()
    {
        if(NewUpgradeCost() > LevelManager.main.coins)
        {
            return;
        }

        LevelManager.main.spendCoins(NewUpgradeCost());

        buildingLvl++;

        bulletsPerSec = NewBulletsPerSec();
        turretRange = NewTurretRange();

        CloseUpgradeUI();
    }
    
    private float NewTurretRange()
    {
        return baseturretRange * Mathf.Pow(buildingLvl, upgradeRangeTowerFactor);
    }
    private float NewBulletsPerSec()
    {
        return baseBps * Mathf.Pow(buildingLvl, upgradeBpsTowerFactor);
    }
    private int NewUpgradeCost()
    {
        return Mathf.RoundToInt(upgradeCost * Mathf.Pow(buildingLvl, upgradeCostFactor));
    }
    public void OpenUpgradeUI()
    {
        UpgradeUI.SetActive(true);
    }

    public void CloseUpgradeUI()
    {  
        UpgradeUI.SetActive(false);
        UIManage.main.SetHoverState(false);
    }

    private void Update()
    {
        if (targetEnemy == null)
        {
            FindEnemy();
            return;
        }
        AimAtTarget();

        if (!CheckIfEnemyIsInRange())
        {
            targetEnemy = null;
        }
        else
        {
            timeUntilAttack += Time.deltaTime;
            if(timeUntilAttack >=  1f / bulletsPerSec) 
            {
                StartShooting();
                timeUntilAttack = 0f;
            }
        }
    }

    private void FindEnemy()
    {
        // Rzuca okr¹g³y promieñ z pozycji wie¿yczki, o promieniu 'turretRange', nie przemieszczaj¹c siê (odleg³oœæ 0),
        // sprawdzaj¹c kolizje z obiektami wrogów okreœlonymi przez 'enemyMask'.
        // Zwraca tablicê wszystkich trafionych obiektów.
        RaycastHit2D[] hits = Physics2D.CircleCastAll(_cachedTransform.position, turretRange, (Vector2)_cachedTransform.position, 0f, enemyMask);
        
        if (hits.Length > 0)
        {
            targetEnemy = hits[0].transform;
            
        }
    }

    private void StartShooting()
    {
        GameObject bulletObject = ObjectPoolManager.SpawnObject(bulletPref, turretRotationPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();
        bulletScript.SetTarget(targetEnemy);
    }

    private bool CheckIfEnemyIsInRange()
    {
        return Vector2.Distance(targetEnemy.position, _cachedTransform.position) <= turretRange;
    }

    private void AimAtTarget()
    {
        float angle = Mathf.Atan2(targetEnemy.position.y - _cachedTransform.position.y, targetEnemy.position.x - _cachedTransform.position.x) * Mathf.Rad2Deg;
        Quaternion turretRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, turretRotation, speedOfRotation * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
            Handles.color =Color.red;
            Handles.DrawWireDisc(transform.position, transform.forward, turretRange);
        #endif
    }
}
