﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public static WeaponManager Instance;

	WeaponStatsSO weaponStats;

	List<RangedWeapon> weapons;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}
		weaponStats = Resources.Load<WeaponStatsSO>("ScriptableObjects/WeaponStats");

		weapons = new List<RangedWeapon>();
		Object[] objects = Resources.LoadAll("Weapons");

		foreach(Object obj in objects)
		{
			GameObject weapon = (GameObject)obj;
			weapon = Instantiate(weapon, transform.position, transform.rotation, this.transform);
			weapon.SetActive(false);
			RangedWeapon rangedWeapon = weapon.GetComponent<RangedWeapon>();
			RangedWeaponStats stats = new RangedWeaponStats();
			foreach(RangedWeaponStats s in weaponStats.stats)
			{
				if(s.name == rangedWeapon.name)
				{
					stats = s;
					break;
				}
			}
			rangedWeapon.stats = stats;

			weapons.Add(rangedWeapon);
			
		}
	}

	public GameObject GetWeapon(string tag)
	{
		foreach(RangedWeapon weapon in weapons)
		{
			print(weapon.name);
			if(weapon.name == tag)
			{
				//Make sure that the ammo is reset
                weapon.stats.currentAmmo = weapon.stats.maxAmmo;
                return weapon.gameObject;
			}
		}
		return null;
	}

}
