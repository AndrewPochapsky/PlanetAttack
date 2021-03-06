﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    public enum Direction { LEFT, RIGHT }

    public enum HoverState { Ready, Recharging, CoolDown }

    HoverState hoverState;

    public Arm arm { get; private set; }

    private DetectGround DG;

    public static Direction direction { get; private set; }

	Player player;
    
    [HideInInspector]
    public Transform weapon;
    Rigidbody2D rb;

    float maxHoverFuel = 27f;
    float fuelDrainAmount = 48;
    float fuelGainAmount = 40;
    float currentHoverFuel;
    float hoverAcceleration = 15;
    float hoverCDDuration = 2f;

    private void Awake()
    {
        DG = transform.GetChild(0).GetComponent<DetectGround>();
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Use this for initialization
    void Start () {
        arm = transform.GetChild(1).GetComponent<Arm>();
        if(arm.transform.GetChild(0).childCount > 0)
            weapon = arm.transform.GetChild(0).GetChild(0);

        direction = Direction.RIGHT;
        hoverState = HoverState.Ready;
        currentHoverFuel = maxHoverFuel;
	}
	
	// Update is called once per frame
	void Update () {
        print("Hover state: " + hoverState);
        Move();
        CheckIfReflectPlayer();
        
	}

    private void FixedUpdate()
    {
        Hover();
    }


    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * player.data.Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * player.data.Speed * Time.deltaTime);
        }
    }

    private void Hover()
    {
        if(Input.GetKey(KeyCode.Space) && hoverState == HoverState.Ready)
        {
            if (currentHoverFuel > 0)
            {
                rb.AddRelativeForce(Vector2.up * hoverAcceleration);
                currentHoverFuel -= fuelDrainAmount * Time.fixedDeltaTime;
            }
            else
            {
                //Fuel fully drained, put hover on cooldown
                hoverState = HoverState.CoolDown;
                StartCoroutine(RechargeCD(hoverCDDuration));
            }
        }
 
        if (DG.IsGrounded)
        {  
            //Fuel has been recharged, hover is ready
            if(currentHoverFuel >= maxHoverFuel)
            {
                hoverState = HoverState.Ready;
            }
            if (hoverState == HoverState.Recharging && currentHoverFuel < maxHoverFuel)
            {
                currentHoverFuel += fuelGainAmount * Time.deltaTime;
                if (currentHoverFuel > maxHoverFuel)
                {
                    currentHoverFuel = maxHoverFuel;
                }
            }
        }
    }

    private IEnumerator RechargeCD(float duration)
    {
        yield return new WaitForSeconds(duration);
        hoverState = HoverState.Recharging;
    }

    //TODO: Eventually look at removing the "speed switching" when the mouse is == to clamp position
    //TODO: Change the player's sprites to rotate
    /// <summary>
    /// Flips the player by changing its sprites, when the mouse passes specific threshholds
    /// </summary>
    private void CheckIfReflectPlayer()
    {
        if (direction == Direction.RIGHT)
        {
            //print("Current: " + arm.transform.localEulerAngles.z + "\n Max: " + arm.MaxClamp);
            if (Mathf.Round(arm.transform.localEulerAngles.z) >= arm.MaxClamp)
            {
                arm.transform.localPosition = new Vector3(
                    -arm.transform.localPosition.x,
                    arm.transform.localPosition.x,
                    arm.transform.localPosition.z);

                arm.MinClamp = 10;
                arm.MaxClamp = 140;


                //print(weapon.localEulerAngles);
                direction = Direction.LEFT;
            }
            if(weapon != null)
                weapon.localEulerAngles = new Vector3(0, 0, 90);

        }
        else if (direction == Direction.LEFT)
        {
            //print("Current: " + arm.transform.eulerAngles.z + "\n Min: " + arm.MinClamp);
            if (Mathf.Round(arm.transform.localEulerAngles.z) <= arm.MinClamp + 3)
            {
                arm.transform.localPosition = new Vector3(
                    -arm.transform.localPosition.x,
                    arm.transform.localPosition.y,
                    arm.transform.localPosition.z);

                arm.MinClamp = 220;
                arm.MaxClamp = 350;

                //print(weapon.localEulerAngles);
                direction = Direction.RIGHT;
            }
            weapon.localEulerAngles = new Vector3(180, 0, -90);
        }
    }
}
