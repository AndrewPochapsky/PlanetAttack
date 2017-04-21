using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
 
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Debug.Log("Loading level: " + levelName);
    }

    public void Quit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    } 


}
