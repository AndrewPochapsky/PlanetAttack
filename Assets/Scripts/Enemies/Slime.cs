using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy {

    private void Awake()
    {
        Health = 5;
        Speed = 5;
        JumpStrength = 2f;

        DG = transform.GetChild(0).GetComponent<DetectGround>();
        RB = GetComponent<Rigidbody2D>();
    }


    // Use this for initialization
    void Start () {
        planet = GameObject.FindObjectOfType<Planet>();
        player = GameObject.FindObjectOfType<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
        
        //Jump();
	}

    private void FixedUpdate()
    {
        InduceGravity();
    }
}
