using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO: add functionality such that the enemy rotates when hit from behind
public class Enemy : Entity, IPoolable {
    
    public enum EnemyType { Slime }
    public enum State { Moving, Attacking }

    private State state;

    protected int NumOfXPOrbs { get; set; }

    protected List<Transform> waypoints;

    public AudioClip hitClip, dieClip;
    private AudioSource audioSource;

    private Text levelText;

    //Transform nearestWaypoint;
    //Vector2 initialDirection;
    Vector2 currentDirection;


    // Use this for initialization
    protected override void Awake () {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        data.CurrentXP = DifficultyController.CurrentXP;
        levelText = canvas.transform.GetChild(0).GetComponent<Text>();
    }
	
    protected override void Update()
    {
        base.Update();

        HandleStates();
        
    }

    //Reset everything
    public virtual void OnObjectSpawn()
    {
        data.CurrentHealth = data.MaxHealth;
        currentDirection = GetInitialDirection();
        state = State.Moving;
    }

    //Super ghetto implementation of state pattern
    //Idealy should use a class for each state: 
    //http://www.gameprogrammingpatterns.com/state.html
    private void HandleStates()
    {
        switch (state)
        {
            case State.Moving:
                transform.Translate(currentDirection * data.Speed * Time.deltaTime);
                break;

            case State.Attacking:
                break;
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.GetComponent<Projectile>();
        if(projectile != null)
        {
            RecieveDamage(projectile.Damage);
            projectile.gameObject.SetActive(false);

            if(CheckIfRotate(projectile))
            {
                Rotate();
            }
        }
    }

    //TOOD: rotate sprites and whatever ever else has to as well
    protected void Rotate()
    {
        currentDirection = -currentDirection;
    }

    /// <summary>
    /// Used to determine if the enemy should change direction when hit with projectile
    /// </summary>
    /// <param name="projectile">The fired projectile</param>
    /// <returns> Return true when enemy and player are facing same direction</returns>
    protected virtual bool CheckIfRotate(Projectile projectile)
    {
        return (projectile.Direction == PlayerMovementController.Direction.LEFT && currentDirection == Vector2.left) 
            || (projectile.Direction == PlayerMovementController.Direction.RIGHT && currentDirection == Vector2.right);
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (!player.IsInvulnerable())
            {
                print(col.gameObject.ToString() + " taking damage");
                player.RecieveDamage(data.Damage);
            }
           
        }
    }

    protected override void Die()
    {
        AudioSource source = ObjectPooler.Instance.SpawnFromPool(nameof(AudioSource), transform.position, transform.rotation).GetComponent<AudioSource>();
        source.clip = dieClip;
        source.Play();

        Player.Instance.IncrementCoins(UnityEngine.Random.Range(data.MinCoins, data.MaxCoins + 1));
        
        //Call function which returns a string

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

    private Vector2 GetInitialDirection()
    {
        int num = UnityEngine.Random.Range(0, 2);
        if(num == 0)
        {
            return Vector2.right;
        }
        else
        {
            return Vector2.left;
        }
    }
}
    




