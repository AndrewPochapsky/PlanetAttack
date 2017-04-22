using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreature : MonoBehaviour {

    public int Health { get; set; }
    public float Speed { get; set; }
    public Rigidbody2D RB { get; set; }


    // Use this for initialization
    void Start () {
        RB = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void Move()
    {

    }


}
