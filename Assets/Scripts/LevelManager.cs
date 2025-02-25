using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] int startCoins = 200;
    public static LevelManager main;

    public Transform startingPoint;
    public Transform[] pathPoints;

    public Transform startingPoint2;
    public Transform[] pathPoints2;
    public int coins;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        coins = startCoins;
    }
    private void Update()
    {
        if (coins >= 500)
        {
            coins = 500;
        }
    }
    public void addCurrency(int profit)
    {
        coins += profit;
    }

    public bool spendCoins(int amount)
    {
        if (coins >= amount) 
        { 
            coins -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough coins.");
            return false;
        }
        
    }
}
