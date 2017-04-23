using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour {

    protected int CurrentHealth { get; set; }
    protected int MaxHealth { get; set; }
    protected float Speed { get; set; }
    protected float JumpStrength { get; set; }
    protected int Level { get; set; }
    protected int CurrentXP { get; set; }
    protected int RequiredXP { get; set; }

    protected DetectGround DG;
    protected Rigidbody2D RB { get; set; }

    protected Planet planet { get; set; }
    protected int Damage { get; set; }
    
    protected virtual void Update()
    {
        if (CurrentHealth <= 0)
        {
            Die();
        }
        if (CanLevelUp())
        {
            LevelUp();
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
        CurrentHealth -= damage;
    }
    protected virtual void LevelUp()
    {
        //in case of overflow exp
        CurrentXP -= RequiredXP;
        Level++;
        RequiredXP = 100 + (50 * (Level - 1));
        CurrentHealth = MaxHealth;
    }

    protected bool CanLevelUp()
    {
        if(CurrentXP >= RequiredXP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
   
    public int GetCurrentHealth()
    {
        return CurrentHealth;
    }
    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public int GetCurrentXP()
    {
        return CurrentXP;
    }
    public virtual void AddXP(int xp)
    {
        CurrentXP += xp;
    }
    public int GetRequiredXP()
    {
        return RequiredXP;
    }
    public int GetLevel()
    {
        return Level;
    }




}
