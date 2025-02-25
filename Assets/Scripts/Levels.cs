using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    int levelsUnlocked;
    public Button[] buttons;
    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }        
        
        for(int i = 0; i < levelsUnlocked && i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void ResetGame()
    {
        PlayerPrefs.SetInt("levelsUnlocked", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
