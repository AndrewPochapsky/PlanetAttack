using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {
    
    public float fadeInTime;
    public static bool isActive = true;
    //private Image fadePanel;
    private Color currentColor = Color.black;
    private float timeSinceSceneStart = 0;
    private Image fadePanel;
    void Awake()
    {
       
        Time.timeScale = 1;
        timeSinceSceneStart = 0;
        
        Debug.Log("This is being started");
    }

	void Start()
    {
        fadePanel = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.timeSinceLevelLoad < fadeInTime)
        {
            //fade in
            
            timeSinceSceneStart += Time.deltaTime;
            float alphaChange = Time.deltaTime / fadeInTime;
            currentColor.a -= alphaChange;
            fadePanel.color = currentColor;
        }
        else
        {
          
            //timeSinceSceneStart = 0;
            Destroy(gameObject);
        }
        //gameObject.SetActive(isActive);
    }
}
