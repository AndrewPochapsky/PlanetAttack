using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FauxGravityBody))]
public class Entity : MonoBehaviour {

    public EntityData data;

    //public Rigidbody2D rb { get; protected set; }
    protected SpriteRenderer sp;
    protected Canvas canvas;

    protected virtual void Awake()
    {
        data = new EntityData();
        sp = GetComponent<SpriteRenderer>();
        //rb = GetComponent<Rigidbody2D>();

        canvas = transform.GetChild(0).GetComponent<Canvas>();
    }
    
    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void RecieveDamage(int damage)
    {
        data.CurrentHealth -= damage;
        if (data.CurrentHealth <= 0)
        {
            Die();
        }
    }
}
