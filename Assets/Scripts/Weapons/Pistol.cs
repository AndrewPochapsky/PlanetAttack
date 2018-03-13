using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RangedWeapon {

    // Use this for initialization
    void Awake() {
		name = "Pistol";
		/*stats.name = "pistol";
		stats.fireSpeed = 20f;
        stats.damage = 10;
        stats.fireRate = 0.5f;
        stats.maxAmmo = 10000;
        stats.currentAmmo = stats.maxAmmo;*/
	}

    public override void Upgrade()
    {
       
    }


	
}
