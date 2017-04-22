using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArm : MonoBehaviour {

    Player player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<Player>();
		
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.position = player.transform.position;

        /* int z = (int)(player.transform.eulerAngles.z / 10);
         z= Mathf.RoundToInt(z);
         z *= 10;*/
        float z = Mathf.Floor(player.transform.eulerAngles.z / 3) * 3;

        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
        transform.rotation = Quaternion.Lerp(transform.rotation,player.transform.rotation, Time.time * 0.5f);
	}
}
