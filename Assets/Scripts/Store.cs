using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

//Game currently keeps running when player is in the store
//Either figure out a way to pause the game or leave it
public class Store : MonoBehaviour, IInteractable {
    public const int MaxUpgradeLevel = 5;

	[SerializeField]
	private Transform storeWindow, scrollViewContent;

	[SerializeField]
	private TextMeshProUGUI notEnoughCoinsText;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		storeWindow.gameObject.SetActive(false);
	}

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Start()
	{
		GenerateStoreOptions();
	}

    public void Interact()
    {
		//TODO: create some sort of global bool to stop stuff from happening when interacting with store.
		//Time.timeScale = 0 doesnt work since it prevents this interaction with continuing when it is.
		if(Input.GetKeyDown(KeyCode.E))
		{
			bool value = !storeWindow.gameObject.activeInHierarchy;
			storeWindow.gameObject.SetActive(value);

		}
    }

	private void GenerateStoreOptions()
	{
		WeaponStatsSO statsSO = Resources.Load<WeaponStatsSO>("ScriptableObjects/WeaponStats");
		
		GameObject upgradeRow = Resources.Load<GameObject>("Store/UpgradeRow");
		foreach(RangedWeaponStats stats in statsSO.stats)
		{
			UpgradeRow row = Instantiate(upgradeRow, scrollViewContent).GetComponent<UpgradeRow>();

            RangedWeapon weapon = WeaponManager.Instance.GetWeapon(stats.name);

			row.SetText(weapon.stats.name, weapon.stats.currentUpgradeCost, weapon.stats.upgradeLevel);
			row.upgradeButton.onClick.AddListener(()=> OnUpgradeButtonPressed(stats.name, row));
		}
	}

    public void OnUpgradeButtonPressed(string weaponName, UpgradeRow row)
    {
        RangedWeapon weapon = WeaponManager.Instance.GetWeapon(weaponName);
        if (Player.Instance.Coins >= weapon.stats.currentUpgradeCost && weapon != null)
        {
            weapon.Upgrade();
            row.SetText(weapon.stats.name, weapon.stats.currentUpgradeCost, weapon.stats.upgradeLevel);
        }
        else if(weapon == null)
        {
            Debug.LogWarning("Weapon: " + name + " can not be found");
        }
		else
		{
			Sequence sequence = DOTween.Sequence()
				.Append(notEnoughCoinsText.DOFade(1, 0.5f))
				.AppendInterval(1f)
				.Append(notEnoughCoinsText.DOFade(0, 0.5f));
		}
		if(weapon.stats.upgradeLevel == MaxUpgradeLevel)
		{
			row.OnUpgradeLimitReached();
		}
    }

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.GetComponent<Player>())
		{
			storeWindow.gameObject.SetActive(false);
		}
	}
}
