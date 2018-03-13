using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

   
    public Vector3 mouseDirection;

    public float MinClamp { get; set; } = 220;
    public float MaxClamp { get; set; } = 350;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
      
    }


	// Update is called once per frame
	void Update () {
		Rotate();
	}

    private void Rotate()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = (mousePos - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.localEulerAngles = new Vector3(
          transform.localEulerAngles.x,
          transform.localEulerAngles.y,
          ClampAngle(transform.localEulerAngles.z, MinClamp, MaxClamp)
     );
    }

    //acquired from here: 
    //https://answers.unity.com/questions/141775/limit-local-rotation.html
    float ClampAngle(float angle, float min, float max) 
    {
 
        if (angle < 90 || angle > 270){       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);

        if (angle < 0) 
            angle += 360;  // if angle negative, convert to 0..360
     
        return angle;
    }
}
