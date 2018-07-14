using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RangedWeaponStats {

    public enum AmmoType { Bullet }

    public string name;
    public AmmoType ammoType;
    public int damage;
    public float knockBack; 
    public float fireSpeed, fireRate;
    public int maxAmmo;
    public int upgradeLevel, baseUpgradeCost, upgradeIncrement;
	//These are calculated in script
	[HideInInspector]
	public int currentUpgradeCost, currentAmmo;
}
