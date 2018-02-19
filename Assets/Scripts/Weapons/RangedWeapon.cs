using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour {
    
    public string Name { get; protected set; }
    public int Damage { get; protected set; }
    public float KnockBack { get; protected set; }
    public float FireRate { get; protected set; }
    public int MaxAmmo { get; protected set; }
    public int CurrentAmmo { get; protected set; }

    private float nextFire = 0;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Time.time > nextFire && Input.GetMouseButton(0))
        {
            //Fire
            //Use object pooling
            nextFire = Time.time + FireRate;
        }
    }


}
