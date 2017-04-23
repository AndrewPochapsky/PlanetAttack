using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour {

    Player player;

    public Text healthText, xpText, levelText, timeText, levelUpText;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
  	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = "Health: " + player.GetCurrentHealth() + "/" + player.GetMaxHealth();
        xpText.text = "Current XP: " + player.GetCurrentXP() + "/" + player.GetRequiredXP();
        levelText.text = "Level: " + player.GetLevel();
        timeText.text = Time.timeSinceLevelLoad.ToString("F2");
	}

    public IEnumerator DisplayLevelUpText()
    {
        levelUpText.text = "Level Up!";
        levelUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        levelUpText.gameObject.SetActive(false);
    }

}
