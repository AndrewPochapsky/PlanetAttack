using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public EntityData data { get; set; }

    private bool dead = false;

    protected DetectGround DG;
    public Rigidbody2D rb { get; protected set; }
    protected SpriteRenderer sp;
    protected Collider2D col;
    protected Canvas canvas;

    //TODO: move to Enemy Script
    protected int Damage { get; set; }
    
    protected virtual void Update()
    {
        if (data.CurrentHealth <= 0 && !dead)
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
        rb = GetComponent<Rigidbody2D>();
    
        canvas = transform.GetChild(0).GetComponent<Canvas>();
            
    }

    protected virtual void FixedUpdate()
    {
        Planet.Instance.InduceGravity(this);
    }

    protected virtual void Die()
    {
        sp.enabled = false;
        col.enabled = false;
        canvas.enabled = false;
        Invoke("Remove", 3);
    }

    protected void Jump()
    {
        if (DG.getGrounded())
        {
            rb.AddForce(Vector2.up * data.JumpStrength, ForceMode2D.Impulse);
        }
    }

    public virtual void RecieveDamage(int damage)
    {
        data.CurrentHealth -= damage;
    }
    protected virtual void LevelUp()
    {
        //in case of overflow exp
        data.CurrentXP -= data.RequiredXP;
        data.Level++;
        SetStats();
    }

    protected bool CanLevelUp()
    {
        return data.CurrentXP >= data.RequiredXP;
    }
   
    public virtual void AddXP(int xp)
    {
        data.CurrentXP += xp;
    }
    
    protected virtual void SetStats()
    {
        data.RequiredXP = 50 + (10 * (data.Level - 1));
        data.MaxHealth = 5 + (3 * (data.Level - 1));
        data.Speed = 2 + (0.5f * (data.Level - 1));
        Damage = 2 + (1 * (data.Level - 1));

        data.CurrentHealth = data.MaxHealth;

    }
    
    private void Remove()
    {
        Destroy(gameObject);
    }
}
