﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
    public override void OnObjectSpawn(){ }
}
