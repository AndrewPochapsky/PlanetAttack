using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour {
    public bool IsGrounded { get; private set; } = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FauxGravityAttractor>())
        {
            IsGrounded = true;
        } 
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<FauxGravityAttractor>())
        {
            IsGrounded = false;
        }
    }
}
