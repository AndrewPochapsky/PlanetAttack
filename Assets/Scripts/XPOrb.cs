﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPOrb : Collectible {

    private void Awake()
    {
        Value = Random.Range(5, 15);
    }

    protected override void Start()
    {
        base.Start();
    }


}
