using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

    private PlayerUIController controller;

    public AudioClip levelUpClip;
    private AudioSource audioSource;

    private LevelManager levelManager;

    private Attack lastAttackUsed;
    private Vector3 RotationVector;
    private bool rotationSet = false;

    private float rotation;

    public Transform hand;
    private Weapon weapon;

    private float invulnerabilityTimer = 0.5f;
    private bool invulnerable = false;
    
    private void Awake()
    {
        Level = 1;
        CurrentXP = 0;
        RequiredXP = 100 + (25 * (Level - 1));
        CurrentHealth = 12;
        MaxHealth = CurrentHealth;
        Speed = 5;
        JumpStrength = 15;
        RB = GetComponent<Rigidbody2D>();
        DG = transform.GetChild(0).GetComponent<DetectGround>();
        //hand = transform.GetChild(0);
        weapon = hand.GetChild(0).GetComponent<Weapon>();
        //Physics2D.gravity = Vector2.zero;
    }

    // Use this for initialization
    void Start () {
        planet = GameObject.FindObjectOfType<Planet>();
        RotationVector = hand.rotation.eulerAngles;
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();
        controller = GameObject.FindObjectOfType<PlayerUIController>();

    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        
        //weapon.transform.rotation = Quaternion.Euler(transform.rotation.x, rotation, transform.rotation.z);
        //SetZRotation(hand);
        //hand.transform.rotation = transform.rotation;
        Move();
	}
    public Attack GetLastAttack()
    {
        return lastAttackUsed;
    }

    private void FixedUpdate()
    {
        InduceGravity();
    }

    protected override void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation = 180;
            transform.Translate(Vector2.left * Speed * Time.deltaTime);


        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation = 0;
            transform.Translate(Vector2.right * Speed * Time.deltaTime);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Attack attack = weapon.Attacks[0];
            Attack(attack,"left");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Attack attack = weapon.Attacks[0];
            Attack(attack,"right");
        }



    }

    private void Attack(Attack attack, string suffix)
    {
        weapon.anim.SetTrigger(attack.GetName()+suffix);
        lastAttackUsed = attack;
    }

    public override void RecieveDamage(int damage)
    {
        base.RecieveDamage(damage);
        StartCoroutine(BecomeInvulerable());
    }

    private IEnumerator BecomeInvulerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTimer);
        invulnerable = false;
    }
    public bool IsInvulnerable()
    {
        return invulnerable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collectible collectible = collision.GetComponent<Collectible>();
        if (collectible && !GetComponent<Weapon>())
        {
            if(collectible is XPOrb)
            {
                //audioSource.clip = xpOrbClip;
                //audioSource.Play();
                AddXP(collectible.GetValue());
               
            }
            collectible.Destroy();
            //Destroy(collectible.gameObject);
            //... more conditions such as health pack
        }
    }
    protected override void Die()
    {
        DifficultyController.SetSurvivedTime(Time.timeSinceLevelLoad.ToString("F2"));
        levelManager.LoadLevel("02End");
        base.Die();
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        audioSource.clip = levelUpClip;
        audioSource.Play();
        StartCoroutine(controller.DisplayLevelUpText());

    }

    protected override void SetStats()
    {
        RequiredXP = 100 + (25 * (Level - 1));
        MaxHealth = 12 + (3 * (Level - 1));
        Speed = 5 + (0.75f * (Level - 1));
        Damage = 2 + (1 * (Level - 1));
        weapon.Attacks[0].SetDamage(weapon.Attacks[0].GetDamage() + (1 * (Level - 1)));
        CurrentHealth = MaxHealth;
    }

    public void SetInvulnerable(bool value)
    {
        invulnerable = value;
    }
}
