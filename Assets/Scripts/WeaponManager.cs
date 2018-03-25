using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

	public static WeaponManager Instance;

	WeaponStatsSO weaponStats;

	public List<RangedWeapon> Weapons { get; private set; }

    private string[] weaponDrops;

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

		Weapons = new List<RangedWeapon>();
		Object[] objects = Resources.LoadAll("Weapons");

		foreach(Object obj in objects)
		{
			GameObject weapon = (GameObject)obj;
			weapon = Instantiate(weapon);
			weapon.transform.position = transform.position;
			weapon.transform.SetParent(this.transform);
			weapon.SetActive(false);
			RangedWeapon rangedWeapon = weapon.GetComponent<RangedWeapon>();
			RangedWeaponStats stats = new RangedWeaponStats();
			foreach(RangedWeaponStats s in weaponStats.stats)
			{
				if(s.name == rangedWeapon.GetType().Name)
				{
					stats = s;
					break;
				}
			}
			rangedWeapon.stats = stats;
			rangedWeapon.CalculateUpgradeCost();
			Weapons.Add(rangedWeapon);
			
		}
        //have to subtract 1 since pistol is not part of drops
        weaponDrops = new string[Weapons.Count - 1];

        int weaponIndex = 0;
        int dropIndex = 0;
        while(weaponIndex < Weapons.Count)
        {
            RangedWeapon weapon = Weapons[weaponIndex];
            if (weapon.GetType() == typeof(Pistol))
            {
                weaponIndex++;
            }
            else
            {
                weaponDrops[dropIndex] = weapon.GetType().Name;
                weaponIndex++;
                dropIndex++;
            }
        }
       
        for(int i = 0; i < weaponDrops.Length; i++)
        {
            print(weaponDrops[i]);
        }
       
	}

	public RangedWeapon GetWeapon(string tag)
	{
		foreach(RangedWeapon weapon in Weapons)
		{
			//print(weapon.name);
			if(weapon.stats.name == tag)
			{
				//Make sure that the ammo is reset
                weapon.stats.currentAmmo = weapon.stats.maxAmmo;
                return weapon;
			}
		}
		return null;
	}

    public string GetWeaponDrop()
    {
        float value = Random.Range(0, 1);

        if(value > 0.5f)
        {
            int index = Random.Range(0, weaponDrops.Length);
            return weaponDrops[index];
        }
        
        return "";
    }

}
