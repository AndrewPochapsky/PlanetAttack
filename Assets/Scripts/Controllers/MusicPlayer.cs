using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour {

    string lastScene = null;

    public AudioClip _00Menu, _01Game, _02End;
    AudioSource audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (lastScene != SceneManager.GetActiveScene().name)
        {
            ChooseClip();
            lastScene = SceneManager.GetActiveScene().name;
        }
	}

    private void ChooseClip()
    {
        AudioClip clip = (AudioClip)this.GetType()
            .GetField(SceneManager.GetActiveScene().name)
            .GetValue(this);

        audioSource.clip = clip;
        audioSource.Play();
    }
}
