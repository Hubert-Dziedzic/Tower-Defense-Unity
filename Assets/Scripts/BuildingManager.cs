using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int currentSelectedBuilding;
    public static BuildingManager main;

    private void Awake()
    {
        main = this;
    }

    public void SetSelectetBuilding(int _currentSelectedBuilding)
    {
        currentSelectedBuilding = _currentSelectedBuilding;
    }

    public Tower GetSelectedBuilding()
    {
        return towers[currentSelectedBuilding];
    }
}
