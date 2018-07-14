using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats")]
public class WeaponStatsSO : ScriptableObject {
    [Header("Initial values for weapon stats, not updated.")]


	public List<RangedWeaponStats> stats;
}

