using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public void Pass()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        Debug.Log("Current Level: " + currentLevel);
        Debug.Log("Levels Unlocked: " + levelsUnlocked);

        if (currentLevel >= levelsUnlocked)
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
            PlayerPrefs.Save();  // Upewnij siê, ¿e dane s¹ zapisane natychmiast
        }

        Debug.Log("New Levels Unlocked: " + PlayerPrefs.GetInt("levelsUnlocked"));
    }
    public void LoadLevelManager()
    {
        SceneManager.LoadScene("Level Manager");
    }
}
