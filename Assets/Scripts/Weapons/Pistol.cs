using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RangedWeapon {

	// Use this for initialization
	void Start () {
		FireSpeed = 5f;
		FireRate = 0.5f;
		MaxAmmo = 10000;
		CurrentAmmo = MaxAmmo;
	}

	
}
