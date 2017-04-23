using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndDisplay : MonoBehaviour {

    public Text congratsText;

	// Use this for initialization
	void Start () {
        congratsText.text = "Congratsulations! You survived for " + DifficultyController.GetSurvivedTime() + " seconds";
    }
	
	// Update is called once per frame
	void Update () {
        
	}
}
