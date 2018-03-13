using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RangedWeaponStats {
    public string name;
	//public SpriteRenderer spriteRenderer;
    public int damage;
    public float knockBack; 
    public float fireSpeed;
    public float fireRate;
    public int maxAmmo;
    public int currentAmmo;
    public int upgradeLevel;
}
