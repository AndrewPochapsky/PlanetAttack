using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    protected override void Awake()
    {
        base.Awake();
        data.JumpStrength = 2f;
        data.Level = 1;
        SetStats();

        NumOfXPOrbs = Random.Range(1, 4);

        DG = transform.GetChild(0).GetComponent<DetectGround>();

        //player = GameObject.FindObjectOfType<Player>();

    }


}
