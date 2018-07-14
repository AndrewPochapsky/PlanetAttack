using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : RangedWeapon {

    public override void Upgrade()
    {
		base.Upgrade();
		CalculateUpgradeCost();
       	print("Pistol Upgraded!");
    }


	
}
