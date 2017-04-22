using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

    Planet planet;
    private bool rotationSet = false;
    private void Awake()
    {
        Speed = 5;
        JumpStrength = 15;
        RB = GetComponent<Rigidbody2D>();
        DG = transform.GetChild(0).GetComponent<DetectGround>();
        //Physics2D.gravity = Vector2.zero;
    }

    // Use this for initialization
    void Start () {
        planet = GameObject.FindObjectOfType<Planet>();
        

	}
	
	// Update is called once per frame
	void Update () {
        if (!DG.getGrounded())
        {
            RB.freezeRotation = true;
        }
        else
        {
            RB.freezeRotation = false;
        }
        if (RB.freezeRotation)
        {
            print("Rotation frozen");
        }
        else
        {
            print("Rotation not frozen");
        }
        Move();
	}


    private void FixedUpdate()
    {
        InduceGravity(planet);
    }

    protected override void Move()
    {
        if (Input.anyKey)
        {

            if (Input.GetKey(KeyCode.A))
            {
                //RB.MovePosition(Vector2.left * Speed * Time.deltaTime);
                //RB.MovePosition(RB.position + (Vector2.left * Speed * Time.deltaTime));

                transform.Translate(Vector2.left * Speed * Time.deltaTime);
                RB.freezeRotation = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //RB.MovePosition(RB.position + (Vector2.right* Speed * Time.deltaTime));

                //RB.MovePosition(Vector2.right * Speed * Time.deltaTime);
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
                RB.freezeRotation = false;
            }
            /*
            if (Input.GetKeyDown(KeyCode.W) && DG.getGrounded())
            {
               
                RB.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
                
                //RB.velocity = new Vector3(0, JumpStrength, 0);
            }
            else
            {
                //RB.freezeRotation = false;
            }*/
        }
       
        

    }

    

}
