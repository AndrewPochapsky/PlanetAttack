using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public static Portal Instance;

    private GameObject innerPortal;
    public bool Activated { get; private set; } = false;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        innerPortal = transform.GetChild(0).gameObject;
        innerPortal.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Activated)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //Go to next level
                print("Going to next level");
            }
        }
    }

    public void Activate()
    {
        print("activating portal");
        Activated = true;
        innerPortal.SetActive(true);
    }

}
