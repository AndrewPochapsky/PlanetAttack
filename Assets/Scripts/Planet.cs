using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    
    
    readonly float gravity = 150;

    public static Planet Instance;


	// Use this for initialization
	void Start () {
		if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}
	
    public void InduceGravity(Entity entity)
    {
        Vector3 directionToPlanet = transform.position - entity.transform.position;
        directionToPlanet.Normalize();
        Vector3 gravityAcc = directionToPlanet * gravity;

        entity.rb.AddForce(gravityAcc, ForceMode2D.Force);
    }
}
