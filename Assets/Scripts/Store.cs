using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour, IInteractable {
    public void Interact()
    {
		if(Input.GetKeyDown(KeyCode.E))
		{
			print("Pressed E!");
		}

    }
}
