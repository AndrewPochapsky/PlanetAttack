using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    private void Awake()
    {
        CurrentHealth = 5;
        MaxHealth = CurrentHealth;
        Speed = 2;
        JumpStrength = 2f;
        Damage = 2;
        Level = 1;
        RequiredXP = 50 + (15 * (Level - 1)); 

        NumOfXPOrbs = Random.Range(1, 3);


        DG = transform.GetChild(0).GetComponent<DetectGround>();
        RB = GetComponent<Rigidbody2D>();

        planet = GameObject.FindObjectOfType<Planet>();
        player = GameObject.FindObjectOfType<Player>();

    }


    // Use this for initialization
    protected override void Start () {
        base.Start();
       
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        Move();
        //Jump();
	}

    private void FixedUpdate()
    {
        
        InduceGravity();
    }


    protected override void LevelUp()
    {
        CurrentXP -= RequiredXP;
        Level++;
        RequiredXP = 50 + (15 * (Level - 1));
        CurrentHealth = MaxHealth;
    }

}
