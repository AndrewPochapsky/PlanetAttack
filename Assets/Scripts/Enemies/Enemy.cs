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
	
	// Update is called once per frame
	void Update () {
        print("yes");
	}

    protected void Attack()
    {

    }

    protected override void Move()
    {
        //transform.position = closestWaypoint().position;
        //transform.Translate(Vector2.MoveTowards(transform.position, closestWaypoint().position, Speed * Time.deltaTime));

    }

    private Transform closestWaypoint()
    {
        //Transform[] closestWaypoints = new Transform[2];
        Transform closestWaypoint = null;
        float distance;
        float shortestDistance=9999;
        foreach(Transform wp in waypoints)
        {
            distance = Vector2.Distance(wp.position, transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                closestWaypoint = wp;
            }
        }
        return closestWaypoint;
    }


}
    




