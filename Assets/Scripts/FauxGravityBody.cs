using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour {

    FauxGravityAttractor attractor;
    
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        attractor = FauxGravityAttractor.Instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        attractor.Attract(transform);
	}
}
