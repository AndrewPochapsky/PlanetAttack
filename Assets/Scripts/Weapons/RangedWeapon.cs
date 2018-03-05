using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour {
    
    public string Name { get; protected set; }
    public int Damage { get; protected set; }
    public float KnockBack { get; protected set; }
    public float FireSpeed { get; protected set; }
    public float FireRate { get; protected set; }
    public int MaxAmmo { get; protected set; }
    public int CurrentAmmo { get; protected set; }

    private float nextFire = 0;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    protected virtual void Update()
    {
        if(Time.time > nextFire && Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    protected virtual void Fire()
    {
        //Fire
        //TODO: have some way of specifying the ammo type, probably with an enum
        //so add a new property called AmmoType, replace "Bullet" with that
        GameObject obj = ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, transform.rotation);

        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.rb.velocity = Player.Instance.GetComponent<PlayerMovementController>().arm.mouseDirection * FireSpeed;


        CurrentAmmo--;

        if (CurrentAmmo == 0)
        {
            //Destroy weapon, replace with regular pistol
        }

        nextFire = Time.time + FireRate;
    }


}
