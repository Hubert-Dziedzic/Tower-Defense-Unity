using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Panel pauzy
    public GameObject pauseButton; // Przycisk "Pause"
    public GameObject resumeButton; // Przycisk "Resume"

    private void Start()
    {
        // Ustawienia pocz¹tkowe: przycisk Resume jest wy³¹czony
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; // Zatrzymanie gry
        pauseMenuUI.SetActive(true); // Poka¿ menu pauzy
        pauseButton.SetActive(false); // Ukryj przycisk "Pause"
        resumeButton.SetActive(true); // Poka¿ przycisk "Resume"
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Wznowienie gry
        pauseMenuUI.SetActive(false); // Ukryj menu pauzy
        pauseButton.SetActive(true); // Poka¿ przycisk "Pause"
        resumeButton.SetActive(false); // Ukryj przycisk "Resume"
    }
}
