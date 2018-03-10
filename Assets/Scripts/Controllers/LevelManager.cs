using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
 
    public static LevelManager Instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

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
