using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour {
    
    [HideInInspector]
    public RangedWeaponStats stats;

    private float nextFire = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        stats = new RangedWeaponStats();
    }

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
        projectile.Damage = stats.damage;
        projectile.rb.velocity = Player.Instance.GetComponent<PlayerMovementController>().arm.mouseDirection * stats.fireSpeed;


        stats.currentAmmo--;

        if (stats.currentAmmo == 0)
        {
            //Destroy weapon, replace with regular pistol
        }

        nextFire = Time.time + stats.fireRate;
    }

    public virtual void Upgrade()
    {
        stats.upgradeLevel++;
        Player.Instance.IncrementCoins(-stats.currentUpgradeCost);
    }

    public virtual void CalculateUpgradeCost()
    {
        stats.currentUpgradeCost = stats.baseUpgradeCost + (stats.upgradeIncrement * (stats.upgradeLevel - 1));
    }


}
