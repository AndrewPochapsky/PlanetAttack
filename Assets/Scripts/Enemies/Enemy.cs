using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingCreature {
    protected Player player { get; set; }

    protected List<Transform> waypoints;

	// Use this for initialization
	protected virtual void Start () {
        waypoints = new List<Transform>();
        GameObject[] wayPointObjects= GameObject.FindGameObjectsWithTag("Waypoint");
        foreach(GameObject obj in wayPointObjects)
        {
            Transform wp = obj.transform;
            waypoints.Add(wp);
        }
        //print(waypoints.Count);
	}
	
	
	

    private void OnCollisionStay2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            if (!player.IsInvulnerable())
            {
                print("dealing " + Damage + " damage");
                player.RecieveDamage(Damage);
            }
           
        }
    }


    protected override void Move()
    {
        //Transform test  = closestWaypoint();
        //transform.position = closestWaypoint().position;
        
        if (closestWaypoint() != null && DG.getGrounded())
            transform.position = Vector3.MoveTowards(transform.position, closestWaypoint().position, Speed * Time.deltaTime);
        if (Mathf.Round(transform.position.x) == Mathf.Round(closestWaypoint().position.x))
        {
            print("reached destination");
            transform.position = Vector3.MoveTowards(transform.position, closestWaypoint().position, Speed * Time.deltaTime);

        }

    }

    private Transform closestWaypoint()
    {
        //Transform[] closestWaypoints = new Transform[2];
        Transform closestWaypoint = null;
        
        Transform secondClosestWaypoint = null;
        float distance;
        float shortestDistance=9999;


        foreach(Transform wp in waypoints)
        {
            distance = Vector3.Distance(wp.position, transform.position);
            if (Mathf.Round(wp.position.x) != Mathf.Round(transform.position.x))
            {
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;

                    closestWaypoint = wp;
                }
            }
            
        }
        shortestDistance = 9999;
        foreach(Transform wp in waypoints)
        {

            if (closestWaypoint != null)
            {
                if (wp.position != closestWaypoint.position && Mathf.Round(wp.position.x) != Mathf.Round(transform.position.x))
                {
                    distance = Vector3.Distance(wp.position, transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        secondClosestWaypoint = wp;
                    }
                }
            }
           
           
        }

        //print("First closest "+closestWaypoint.ToString());
        //print("Second closest"+ secondClosestWaypoint.ToString());
        if (closestWaypoint != null)
        {
            float firstDistance = Vector2.Distance(closestWaypoint.position, player.transform.position);
            float secondDistance = Vector2.Distance(secondClosestWaypoint.position, player.transform.position);
            if (firstDistance > secondDistance)
            {
                return secondClosestWaypoint;
            }
            else
            {
                return closestWaypoint;
            }
        }
        else
        {
            return null;
        }
       

    
    }
   


}
    




