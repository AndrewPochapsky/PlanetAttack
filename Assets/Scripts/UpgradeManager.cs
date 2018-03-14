using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour {

    public const int MaxUpgradeLevel = 5;

	public void Upgrade(string tag)
	{
		RangedWeapon weapon = WeaponManager.Instance.GetWeapon(tag);
		if(weapon != null)
		{
			weapon.Upgrade();
		}
		else
		{
            Debug.LogWarning("Weapon: " + name + " can not be found");
        }
	}
}
