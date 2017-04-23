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

    private bool dead = false;

    protected DetectGround DG;
    protected Rigidbody2D RB { get; set; }
    protected SpriteRenderer sp;
    protected Collider2D col;
    protected Canvas canvas;

    protected Planet planet { get; set; }
    protected int Damage { get; set; }
    
    protected virtual void Update()
    {
        if (CurrentHealth <= 0 && !dead)
        {
            dead = true;
            Die();
        }
        if (CanLevelUp())
        {
            LevelUp();
        }
    }

    protected virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        canvas = transform.GetChild(0).GetComponent<Canvas>();
    }

    protected virtual void Die()
    {
        sp.enabled = false;
        col.enabled = false;
        canvas.enabled = false;
        Invoke("Remove", 3);
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
        SetStats();
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
    protected virtual void SetStats()
    {
        RequiredXP = 50 + (10 * (Level - 1));
        MaxHealth = 5 + (3 * (Level - 1));
        Speed = 2 + (0.5f * (Level - 1));
        if((Level-1)%2==0)
            Damage = 2 + (1 * (Level - 1));

        CurrentHealth = MaxHealth;

    }
    private void Remove()
    {
        Destroy(gameObject);
    }




}
