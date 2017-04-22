using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingCreature {
    protected Player player { get; set; }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    protected void Attack()
    {

    }

    protected override void Move()
    {
        print("moving");
        transform.Translate(Vector2.left * Speed * Time.deltaTime);
        
    }



}
