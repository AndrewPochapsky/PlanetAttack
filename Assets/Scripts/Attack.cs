using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack {

    private int Damage { get; set; }
    private float KnockBack { get; set; }
    private string Name { get; set; }

	public Attack(string name, int damage, float knockBack)
    {
        Name = name;
        Damage = damage;
        KnockBack = knockBack;
    }

    public int GetDamage()
    {
        return Damage;
    }
    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    public float GetKnockBack()
    {
        return KnockBack;
    }
    public void SetKnockBack(float knockBack)
    {
        KnockBack = knockBack;
    }

    public string GetName()
    {
        return Name;
    }
    public void SetName(string name)
    {
        Name = name;
    }

}
