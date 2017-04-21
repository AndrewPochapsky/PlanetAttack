using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour {
    private bool isPanelInScene = false;
    public GameObject fadeInPanel, fadeOutPanel;
    private Canvas canvas;
	// Use this for initialization
	void Start () {
        canvas = GameObject.FindObjectOfType<Canvas>();
        GameObject oldPanel = GameObject.FindGameObjectWithTag("Panel");
        if (oldPanel)
        {
            Destroy(oldPanel);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (!isPanelInScene)
        {
            InstantiateFadeInPanel();
        }  

    }

    public void InstantiateFadeInPanel()
    {
        Debug.Log("Instantiates panel");
        GameObject newPanel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
        newPanel.transform.SetParent(canvas.transform, false);

        isPanelInScene = true;
    }
    public void InstantiateFadeOutPanel()
    {
        Debug.Log("Instantiates panel");
        GameObject newPanel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
        newPanel.transform.SetParent(canvas.transform, false);

        isPanelInScene = true;
    }



}
