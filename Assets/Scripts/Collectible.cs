using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    protected int Value { get; set; }

    public int GetValue()
    {
        return Value;
    }

    public void Destroy()
    {
        if (transform.parent != null)
        {
            Transform parent = transform.parent;
            Destroy(parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

}
