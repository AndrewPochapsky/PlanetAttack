using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    protected override void Awake()
    {
        base.Awake();

        data.CurrentHealth = data.MaxHealth;

        NumOfXPOrbs = Random.Range(1, 4);

        //DG = transform.GetChild(0).GetComponent<DetectGround>();
    }

   


}
