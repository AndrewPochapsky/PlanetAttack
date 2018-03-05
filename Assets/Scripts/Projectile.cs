﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Projectile : MonoBehaviour, IPoolable {

    public Rigidbody2D rb { get; protected set; }

    public abstract void OnObjectSpawn();
    
}
