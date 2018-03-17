using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {
    
    
    readonly float gravity = 50;

    #region Singleton

    public static Planet Instance;


	// Use this for initialization
	void Awake () {
		if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}

    #endregion Singleton
	
    public void InduceGravity(Entity entity)
    {
        /*Vector3 directionToPlanet = transform.position - entity.transform.position;
        directionToPlanet.Normalize();
        Vector3 gravityAcc = directionToPlanet * gravity;

        entity.rb.AddForce(gravityAcc, ForceMode2D.Force);*/
    }
}
