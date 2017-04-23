using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    private void Awake()
    {
        
        JumpStrength = 2f;
        Level = 1;
        SetStats();

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
        SetStats();
        print("Damage: " + Damage);
        
    }

    private void SetStats()
    {
        RequiredXP = 50 + (15 * (Level - 1));
        MaxHealth = 5 + (3 * (Level - 1));
        Speed = 2 + (0.25f * (Level - 1));
        Damage = 2 + (1 * (Level - 1));

        CurrentHealth = MaxHealth;

    }


}
