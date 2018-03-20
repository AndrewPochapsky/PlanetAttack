using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class PlayerUIController : MonoBehaviour {

    public TextMeshProUGUI healthText, coinsText, timeText, ammoText;

    private void Awake()
    {
        
        foreach (RangedWeapon weapon in WeaponManager.Instance.Weapons)
        {
            weapon.OnAmmoUpdatedEvent += OnAmmoUpdated;
        }
    }

    // Use this for initialization
    void Start () {
        Player.Instance.OnHealthUpdatedEvent += OnHealthUpdated;
        Player.Instance.OnCoinsUpdatedEvent += OnCoinsUpdated;

    }
	
	void Update () {
        timeText.text = Time.timeSinceLevelLoad.ToString("F2");
	}

    private void OnHealthUpdated(int current, int max)
    {
        healthText.text = "Health: " + current+ "/" + max;
    }

    private void OnCoinsUpdated(int value)
    {
        coinsText.text = "Coins: " + value;
    }

    private void OnAmmoUpdated(int current, int max)
    {
        if(max == -1)
        {
            ammoText.text = "Unlimited";
        }
        else
        {
            ammoText.text = current + "/" + max;
        }
    }

}
