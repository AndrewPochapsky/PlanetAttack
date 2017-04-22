using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon{

	// Use this for initialization
	protected override void Start () {
        base.Start();
        Attacks.Add(new Attack("swipe", 2, 5f));
        
	}
	
	
}
