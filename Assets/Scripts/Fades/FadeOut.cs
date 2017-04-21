using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class FadeOut : MonoBehaviour {


    public float fadeOutTime = 1.5f;
    //private Image fadePanel;
    private Color currentColor = Color.black;
    private float timeSinceFadeStart = 0;
    private Image fadePanel;
    void Awake()
    {
        Time.timeScale = 1;
        timeSinceFadeStart = 0;
        currentColor.a = 0;

    }

    void Start()
    {
        fadePanel = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeSinceFadeStart < fadeOutTime)
        {  
            //fade out
            timeSinceFadeStart += Time.deltaTime;
            float alphaChange = Time.deltaTime / fadeOutTime;
            currentColor.a += alphaChange;
            fadePanel.color = currentColor;
            Debug.Log("Fading");
        }
    }
}
