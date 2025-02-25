using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        // Przywróæ czas do normalnego stanu
        Time.timeScale = 1f;
        // Za³aduj ponownie bie¿¹c¹ scenê
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
