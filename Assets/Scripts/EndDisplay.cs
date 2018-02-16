using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDisplay : MonoBehaviour {

    public Text congratsText;

	// Use this for initialization
	void Start () {
        congratsText.text = "Congratulations! You survived for " + DifficultyController.SurvivedTime + " seconds";
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
