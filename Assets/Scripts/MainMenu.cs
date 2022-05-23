using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void QuitGame() {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void StartGame() {            
        Dropdown[] dropdowns = FindObjectsOfType<Dropdown>();
        int valueOfLevel = dropdowns[0].value;
        string level = "Level" + (valueOfLevel+1).ToString();
        SceneManager.LoadScene(level);
    }
}
