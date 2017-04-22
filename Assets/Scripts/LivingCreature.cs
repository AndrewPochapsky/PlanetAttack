using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour {

    protected int Health { get; set; }
    protected float Speed { get; set; }
    protected float JumpStrength { get; set; }

    protected DetectGround DG;
    protected Rigidbody2D RB { get; set; }

    protected Planet planet { get; set; }
    protected int Damage { get; set; }
    
    protected virtual void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
       
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Move()
    {

    }

    protected void InduceGravity()
    {
        Vector3 directionToPlanet = planet.transform.position - transform.position;
        directionToPlanet.Normalize();
        Vector3 gravityAcc = directionToPlanet * planet.GetGravity();

        RB.AddForce(gravityAcc, ForceMode2D.Force);


    }

    protected void Jump()
    {
        if (DG.getGrounded())
        {
            RB.AddForce(Vector2.up * JumpStrength, ForceMode2D.Impulse);
        }
    }
    public virtual void RecieveDamage(int damage)
    {
        Health -= damage;
    }
   




}
