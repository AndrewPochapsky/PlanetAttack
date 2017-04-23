using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    private void Awake()
    {
        
        JumpStrength = 2f;
        Level = 1;
        SetStats();

        NumOfXPOrbs = Random.Range(1, 4);

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


    

    


}
