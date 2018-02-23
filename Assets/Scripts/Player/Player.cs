using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

    public enum Direction { LEFT, RIGHT }

    public Direction direction;

    private PlayerUIController controller;

    public AudioClip levelUpClip, hitClip;
    
    private AudioSource audioSource;

    private LevelManager levelManager;

    private float invulnerabilityTimer = 0.5f;
    private bool invulnerable = false;

    Arm arm;

    
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
        arm = transform.GetChild(1).GetComponent<Arm>();

        direction = Direction.RIGHT;
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();

        planet = GameObject.FindObjectOfType<Planet>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();
        controller = GameObject.FindObjectOfType<PlayerUIController>();

    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        Move();
        CheckIfReflectPlayer();
	}

    private void FixedUpdate()
    {
        InduceGravity();
    }

    protected override void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Speed * Time.deltaTime);
        }
    }
    /* 
    private void Attack(Attack attack, string suffix)
    {
        weapon.anim.SetTrigger(attack.Name+suffix);
        LastAttack = attack;
    }*/

    public override void RecieveDamage(int damage)
    {
        audioSource.clip = hitClip;
        audioSource.Play();

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
        if (collectible && !GetComponent<RangedWeapon>())
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
        DifficultyController.SurvivedTime = Time.timeSinceLevelLoad.ToString("F2");
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
        RequiredXP = 100 + (65 * (Level - 1));
        MaxHealth = 12 + (3 * (Level - 1));
        Speed = 5 + (0.75f * (Level - 1));
        //Damage = 2 + (1 * (Level - 1));
        //weapon.Attacks[0].Damage = (weapon.Attacks[0].Damage + (1 * (Level - 1)));
        CurrentHealth = MaxHealth;
    }

    public void SetInvulnerable(bool value)
    {
        invulnerable = value;
    }

    //TODO: Eventually look at removing the "speed switching" when the mouse is == to clamp position
    //TODO: Change the player's sprites to rotate
    /// <summary>
    /// Flips the player by changing its sprites, when the mouse passes specific threshholds
    /// </summary>
    private void CheckIfReflectPlayer()
    {
        if (direction == Direction.RIGHT)
        {
            //print("Current: " + arm.transform.localEulerAngles.z + "\n Max: " + arm.MaxClamp);
            if(Mathf.Round(arm.transform.localEulerAngles.z) >= arm.MaxClamp)
            {
                arm.transform.localPosition = new Vector3(
                    -arm.transform.localPosition.x, 
                    arm.transform.localPosition.x, 
                    arm.transform.localPosition.z );

                arm.MinClamp = 10;
                arm.MaxClamp = 140;

                direction = Direction.LEFT;
            }
        }
        else if(direction == Direction.LEFT)
        {
            //print("Current: " + arm.transform.eulerAngles.z + "\n Min: " + arm.MinClamp);
            if (Mathf.Round(arm.transform.localEulerAngles.z) <= arm.MinClamp + 3)
            {
                arm.transform.localPosition = new Vector3(
                    -arm.transform.localPosition.x,
                    arm.transform.localPosition.y,
                    arm.transform.localPosition.z);

                arm.MinClamp = 220;
                arm.MaxClamp = 350;

                direction = Direction.RIGHT;
            }
        }
    }
    
}
