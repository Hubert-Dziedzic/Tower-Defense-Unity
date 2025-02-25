using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int maxHp = 10;
    [SerializeField] private float damageRange = 1.0f;
    private int hp;

    [Header("Damage Settings")]
    [SerializeField] private int damageOnCollision = 1;

    [Header("UI Elements")]
    [SerializeField] private GameObject loseScreen;
    [SerializeField] Image imgSliderHp;

    void Start()
    {
        hp = maxHp;
        loseScreen.SetActive(false);
        UpdateUISlider();
    }

    public void TakePlayerDamage(int damage)
    {
        hp -= damage;
        UpdateUISlider();
        if (hp <= 0)
        {
            ShowLoseScreen();
        }
    }

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance <= damageRange)
            {
                TakePlayerDamage(damageOnCollision);
                EnemySpawner.onEnemyKilled.Invoke();
                ObjectPoolManager.ReturnObjectPool(enemy);
            }
        }
    }
    private void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    void UpdateUISlider()
    {
        if(imgSliderHp != null)
        {
            imgSliderHp.fillAmount = (float)hp / maxHp;
            if(imgSliderHp.fillAmount <= 0.3f)
            {
                imgSliderHp.color = Color.red;
            }
            else
            {
                imgSliderHp.color = Color.green;
            }
        }
        
    }
}

