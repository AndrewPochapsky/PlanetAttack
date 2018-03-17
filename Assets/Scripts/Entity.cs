using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    public EntityData data;

    protected DetectGround DG;
    public Rigidbody2D rb { get; protected set; }
    protected SpriteRenderer sp;
    protected Canvas canvas;

    protected virtual void Awake()
    {
        data = new EntityData();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        canvas = transform.GetChild(0).GetComponent<Canvas>();
    }
    
    protected virtual void Update()
    {
        if (CanLevelUp())
        {
            LevelUp();
        }
    }

    protected virtual void FixedUpdate()
    {
        Planet.Instance.InduceGravity(this);
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
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
        if (data.CurrentHealth <= 0)
        {
            Die();
        }
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
        data.Damage = 2 + (1 * (data.Level - 1));

        data.CurrentHealth = data.MaxHealth;

    }
    
}
