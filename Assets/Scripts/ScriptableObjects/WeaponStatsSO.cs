using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats")]
public class WeaponStatsSO : ScriptableObject {
	public List<RangedWeaponStats> stats;
}

