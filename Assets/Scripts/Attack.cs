using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {

    public int Damage { get; set; }
    public float KnockBack { get; set; }
    public string Name { get; private set; }

	public Attack(string name, int damage, float knockBack)
    {
        Name = name;
        Damage = damage;
        KnockBack = knockBack;
    }


    public void IncreaseAttackDamage(int x)
    {
        Damage += x;
    }

}
