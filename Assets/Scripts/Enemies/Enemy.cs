using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingCreature {
    protected Player player { get; set; }
    protected int NumOfXPOrbs { get; set; }
    protected List<Transform> waypoints;

    private Text levelText;
    private Canvas canvas;

    private int count = 0;

    Transform nearestWaypoint;
    Vector2 moveDirection;

    private void Awake()
    {
       
    }

    // Use this for initialization
    protected virtual void Start () {
        CurrentXP = DifficultyController.GetCurrentXP();

        canvas = transform.GetChild(1).GetComponent<Canvas>();
        levelText = canvas.transform.GetChild(0).GetComponent<Text>();


        waypoints = new List<Transform>();
        GameObject[] wayPointObjects= GameObject.FindGameObjectsWithTag("Waypoint");
        foreach(GameObject obj in wayPointObjects)
        {
            Transform wp = obj.transform;
            waypoints.Add(wp);
        }
        //print(waypoints.Count);
        InvokeRepeating("IncrementXP", 0, 1);
        
        nearestWaypoint = closestWaypoint();
        print(nearestWaypoint.ToString());
        moveDirection = ChooseMoveDirection();
    }
	
    protected override void Update()
    {
        transform.Translate(moveDirection* Speed * Time.deltaTime);

        levelText.text = Level.ToString();
        //print("Enemy xp: " + CurrentXP);
        
        //IncrementXP();
        base.Update();
        
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


    private Vector2 ChooseMoveDirection()
    {

        //Depending on which quadrant the enemy is on: dependent on y coord
        //will decide whether or not the enemy should continue going left or right
        //after x amount of waypoints, probably 1 or at most 2
        /*
        Vector3 startingTransform = transform.position;
        //print(startingTransform);
        if (closestWaypoint() != null && DG.getGrounded())
        {
            transform.position = Vector3.MoveTowards(transform.position, closestWaypoint().position, Speed * Time.deltaTime);
            
        }
            
        
        if (Mathf.Round(transform.position.x) == Mathf.Round(closestWaypoint().position.x))
        {/*
            count++;
            if (count == 1)
            {
                print("HAPPENING");
                if(startingTransform.x < transform.position.x && startingTransform.y < -6.57f)
                {
                    transform.Translate(Vector2.left * Speed * Time.deltaTime);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, closestWaypoint().position, Speed * Time.deltaTime);
            }*/
        print("reached destination");
        if (transform.position.x < nearestWaypoint.position.x && transform.position.y < 0f)//
        {
            print("1");
            //transform.Translate(Vector2.left * Speed * Time.deltaTime);
            return Vector2.left;
            
        }
        
        else if (transform.position.x < nearestWaypoint.position.x && transform.position.y > 0)
        {
            print("2");
            //transform.Translate(Vector2.right * Speed * Time.deltaTime);
            return Vector2.right;
        }
       

        else if (transform.position.x > nearestWaypoint.position.x && transform.position.y > 0f)
        {
            print("3");
            //transform.Translate(Vector2.left * Speed * Time.deltaTime);
            return Vector2.left;
            
        }
       
        else if (transform.position.x > nearestWaypoint.position.x && transform.position.y < 0)//
        {
            print("4");
            //transform.Translate(Vector2.right * Speed * Time.deltaTime);
            return Vector2.right;
        }
        
        print("rip");
        return Vector2.zero;




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
    protected override void Die()
    {
        
        for (int i = 0; i < NumOfXPOrbs; i++)
        {
            Vector3 offset = new Vector3(Random.Range(0, 2), Random.Range(0, 2), 0);
            Instantiate(Resources.Load("Collectibles/XPOrbContainer"), transform.position+ offset, transform.rotation);
        }
        base.Die();
    }

    private void IncrementXP()
    {
        AddXP(DifficultyController.GetRate());
    }


}
    




