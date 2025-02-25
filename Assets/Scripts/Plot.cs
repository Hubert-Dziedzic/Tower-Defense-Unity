using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Color hoverColor;
    [SerializeField] private SpriteRenderer sr;

    public GameObject buildingObj;
    public Turret turret;
    private Color color;

    private Transform _cachedTransform;

    private void Awake()
    {
        _cachedTransform = transform;
    }
    private void Start()
    {
        color = sr.color;
    }

    private void OnMouseDown()
    {
        if(UIManage.main.IsHoverUI())
        {
            return;
        }

        if (buildingObj != null)
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower buildingToBuild = BuildingManager.main.GetSelectedBuilding();

        if (buildingToBuild.cost > LevelManager.main.coins)
        {
            Debug.Log("You can't buy this turret");
            return;
        }

        LevelManager.main.spendCoins(buildingToBuild.cost);

        buildingObj = Instantiate(buildingToBuild.pref, _cachedTransform.position, Quaternion.identity);
        turret = buildingObj.GetComponent<Turret>();
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = color;
    }
}
