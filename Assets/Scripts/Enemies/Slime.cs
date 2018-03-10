using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    protected override void Awake()
    {
        base.Awake();
        data.JumpStrength = 2f;
        data.Level = 1;
        data.RequiredXP = 999;
        data.MaxHealth = 10;
        data.Speed = 2;
        Damage = 2;

        MinCoins = 5;
        MaxCoins = 10;

        data.CurrentHealth = data.MaxHealth;

        NumOfXPOrbs = Random.Range(1, 4);

        DG = transform.GetChild(0).GetComponent<DetectGround>();
    }

    public override void OnObjectSpawn()
    {
        data.JumpStrength = 2f;
        data.Level = 1;
        data.RequiredXP = 999;
        data.MaxHealth = 10;
        data.Speed = 2;
        Damage = 2;

        data.CurrentHealth = data.MaxHealth;
    }


}
