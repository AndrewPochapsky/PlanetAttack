using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGround : MonoBehaviour {
    private bool isGrounded = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Planet>())
        {

            isGrounded = true;
        }
       
      
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {   
        if (collision.GetComponent<Planet>())
        {

            isGrounded = false;
        }
        
       

    }

    public bool getGrounded()
    {
        return isGrounded;
    }

}
