using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour {

    public delegate void OnAmmoUpdated(int current, int max);
    public event OnAmmoUpdated OnAmmoUpdatedEvent;

    [HideInInspector]
    public RangedWeaponStats stats;

    Transform exit;

    private float nextFire = 0;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        stats = new RangedWeaponStats();
        exit = transform.GetChild(0);
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
        //TODO: have some way of specifying the ammo type, probably with an enum
        //so add a new property called AmmoType, replace "Bullet" with that
        GameObject obj = ObjectPooler.Instance.SpawnFromPool(stats.ammoType.ToString(), exit.position, transform.rotation);

        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Direction = PlayerMovementController.direction;
        projectile.Damage = stats.damage;
        projectile.rb.velocity = Player.Instance.GetComponent<PlayerMovementController>().arm.mouseDirection * stats.fireSpeed;

        //If ammo is set to -1, then the ammo is infinite
        if(stats.currentAmmo != -1)
        {
            stats.currentAmmo--;
            CallAmmoEvent();
        }
            
        if (stats.currentAmmo <= 0)
        {
            //Destroy weapon, replace with regular pistol
        }

        nextFire = Time.time + stats.fireRate;
    }

    //Required so Player can call this on equip of a new weapon
    public void CallAmmoEvent()
    {
        OnAmmoUpdatedEvent(stats.currentAmmo, stats.maxAmmo);
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
