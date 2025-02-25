using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Przywr�� czas do normalnego stanu
        Time.timeScale = 1f;
        // Za�aduj ponownie bie��c� scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
