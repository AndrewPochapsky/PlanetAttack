using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour, IInteractable {
    public const int MaxUpgradeLevel = 5;

	[SerializeField]
	private Transform storeWindow, scrollViewContent;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Start()
	{
		GenerateStoreOptions();
	}

    public void Interact()
    {
		if(Input.GetKeyDown(KeyCode.E))
		{
			storeWindow.gameObject.SetActive(!storeWindow.gameObject.activeInHierarchy);
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
        if (weapon != null)
        {
            weapon.Upgrade();
            row.SetText(weapon.stats.name, weapon.stats.currentUpgradeCost, weapon.stats.upgradeLevel);
        }
        else
        {
            Debug.LogWarning("Weapon: " + name + " can not be found");
        }
		if(weapon.stats.upgradeLevel == MaxUpgradeLevel)
		{
			row.OnUpgradeLimitReached();
		}
    }
}
