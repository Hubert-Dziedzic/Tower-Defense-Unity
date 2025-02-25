using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level Manager");
    }
    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
