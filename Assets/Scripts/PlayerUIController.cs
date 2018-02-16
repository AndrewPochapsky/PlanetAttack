using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerUIController : MonoBehaviour {

    Player player;

    public Text healthText, xpText, levelText, timeText, levelUpText;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
  	}
	
	//TODO: replace with events
	void Update () {
        healthText.text = "Health: " + player.CurrentHealth + "/" + player.MaxHealth;
        xpText.text = "Current XP: " + player.CurrentXP + "/" + player.RequiredXP;
        levelText.text = "Level: " + player.Level;
        timeText.text = Time.timeSinceLevelLoad.ToString("F2");
	}

    //TODO: Replace with DOTween
    public IEnumerator DisplayLevelUpText()
    {
        levelUpText.text = "Level Up!";
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        levelUpText.gameObject.SetActive(false);
    }

}
