using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

    Planet planet;
    private bool rotationSet = false;
    private static Quaternion roundedRotation;
    private void Awake()
    {
        Speed = 5;
        JumpStrength = 10;
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
        Move();
	}

    public static Quaternion GetRoundedRotation()
    {
        return roundedRotation;
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

            }
            if (Input.GetKey(KeyCode.D))
            {
                //RB.MovePosition(RB.position + (Vector2.right* Speed * Time.deltaTime));

                //RB.MovePosition(Vector2.right * Speed * Time.deltaTime);
                transform.Translate(Vector2.right * Speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.W) && DG.getGrounded())
            {
                RB.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
            }
        }
        else
        {

        }
        

    }

    

}
