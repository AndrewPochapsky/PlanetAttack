using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour {

    public int CurrentHealth { get; protected set; }
    public int MaxHealth { get; protected set; }
    protected float Speed { get; set; }
    protected float JumpStrength { get; set; }
    public int Level { get; protected set; }
    public int CurrentXP { get; protected set; }
    public int RequiredXP { get; protected set; }

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
        if(sp==null)
            sp = GetComponent<SpriteRenderer>();
        if(col==null)
            col = GetComponent<Collider2D>();
        if(canvas==null)
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
        return CurrentXP >= RequiredXP;
    }
   
    public virtual void AddXP(int xp)
    {
        CurrentXP += xp;
    }
    
    protected virtual void SetStats()
    {
        RequiredXP = 50 + (10 * (Level - 1));
        MaxHealth = 5 + (3 * (Level - 1));
        Speed = 2 + (0.5f * (Level - 1));
        Damage = 2 + (1 * (Level - 1));

        CurrentHealth = MaxHealth;

    }
    private void Remove()
    {
        Destroy(gameObject);
    }
}
