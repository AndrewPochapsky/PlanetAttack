using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour {

    public AudioClip menu, game, end;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name=="00Menu")
        {
            audioSource.clip = menu;
            audioSource.Play();
            audioSource.loop = true;
        }
        else if(SceneManager.GetActiveScene().name == "01Game")
        {
            audioSource.clip = game;
            audioSource.Play();
            audioSource.loop = true;
        }
        else if (SceneManager.GetActiveScene().name == "02End")
        {
            audioSource.clip = end;
            audioSource.Play();
            audioSource.loop = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
