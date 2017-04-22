using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

    Planet planet;

    private void Awake()
    {
        Speed = 5;
        RB = GetComponent<Rigidbody2D>();
        Physics2D.gravity = Vector2.zero;
    }

    // Use this for initialization
    void Start () {
        planet = GameObject.FindObjectOfType<Planet>();


	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void FixedUpdate()
    {
        InduceGravity();
    }

    protected override void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //RB.MovePosition(Vector2.left * Speed * Time.deltaTime);
            //RB.MovePosition(RB.position + (Vector2.left * Speed * Time.deltaTime));
            
            transform.Translate(Vector2.left * Speed * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //RB.MovePosition(RB.position + (Vector2.right* Speed * Time.deltaTime));
            
            //RB.MovePosition(Vector2.right * Speed * Time.deltaTime);
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }

    private void InduceGravity()
    {
        Vector2 directionToPlanet = planet.transform.position-transform.position;
        directionToPlanet.Normalize();
        Vector2 gravityAcc = directionToPlanet * planet.GetGravity();

        RB.AddForce(gravityAcc, ForceMode2D.Force);


    }

}
