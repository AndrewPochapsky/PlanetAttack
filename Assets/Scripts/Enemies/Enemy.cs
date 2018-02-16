using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingCreature {
    protected Player player { get; set; }
    protected int NumOfXPOrbs { get; set; }
    protected List<Transform> waypoints;

    

    public AudioClip hitClip, dieClip;
    private AudioSource audioSource;


    protected float HealthModifier { get; set; }
    protected float DamageModifier { get; set; }
    protected float SpeedModifier { get; set; }

    private Text levelText;

    private int count = 0;

    Transform nearestWaypoint;
    Vector2 moveDirection;

    private void Awake()
    {
       
    }

    // Use this for initialization
    protected override void Start () {
        audioSource = GetComponent<AudioSource>();
        CurrentXP = DifficultyController.CurrentXP;

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
        //print(nearestWaypoint.ToString());
        moveDirection = ChooseMoveDirection();
        base.Start();
        levelText = canvas.transform.GetChild(0).GetComponent<Text>();
    }
	
    protected override void Update()
    {
        transform.Translate(moveDirection* Speed * Time.deltaTime);

        levelText.text = Level.ToString();
        base.Update();
        
    }
	
    private void OnCollisionStay2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag=="Player" && collision.gameObject.tag!="Weapon")
        {
            if (!player.IsInvulnerable())
            {
                print(collision.gameObject.ToString() + " taking damage");
                player.RecieveDamage(Damage);
            }
           
        }
    }

    //TODO actually remove this garbage and make it work
    private Vector2 ChooseMoveDirection()
    {

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
        audioSource.clip = dieClip;
        audioSource.Play();
        for (int i = 0; i < NumOfXPOrbs; i++)
        {
            Vector3 offset = new Vector3(0, Random.Range(0,2), 0);
            Instantiate(Resources.Load("Collectibles/XPOrbContainer"), transform.position+ offset, transform.rotation);
        }
        base.Die();
    }

    private void IncrementXP()
    {
        AddXP(DifficultyController.XPRate);
    }

    public override void RecieveDamage(int damage)
    {
        audioSource.clip = hitClip;
        audioSource.Play();
        base.RecieveDamage(damage);
    }
    
}
    




