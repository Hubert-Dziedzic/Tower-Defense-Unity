using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [Header("Attributes of Enemy Health")]
    [SerializeField] private int profitForDestroyingOpponent = 60;
    [SerializeField] private int enemyHP = 2;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        enemyHP -= dmg;

        if (enemyHP == 0 && isDestroyed == false)
        {
            EnemySpawner.onEnemyKilled.Invoke();
            LevelManager.main.addCurrency(profitForDestroyingOpponent);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
