using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : RangedWeapon {

    public override void Upgrade()
    {
        base.Upgrade();
        CalculateUpgradeCost();
        print("Rifle Upgraded!");
    }
}
