using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    private PlayerUIController controller;

    public AudioClip levelUpClip, hitClip;
    
    private AudioSource audioSource;

    private LevelManager levelManager;

    private float invulnerabilityTimer = 0.5f;
    private bool invulnerable = false;

    public static Player Instance;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }



        data = new EntityData();

        data.Level = 1;
        data.CurrentXP = 0;
        data.RequiredXP = 100 + (25 * (data.Level - 1));
        data.CurrentHealth = 12;
        data.MaxHealth = data.CurrentHealth;
        data.Speed = 5;
        data.JumpStrength = 15;
        rb = GetComponent<Rigidbody2D>();
        DG = transform.GetChild(0).GetComponent<DetectGround>();
    }

    // Use this for initialization
    protected override void Start () {

        base.Start();

        levelManager = GameObject.FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();
        controller = GameObject.FindObjectOfType<PlayerUIController>();

    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
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
        data.RequiredXP = 100 + (65 * (data.Level - 1));
        data.MaxHealth = 12 + (3 * (data.Level - 1));
        data.Speed = 5 + (0.75f * (data.Level - 1));
        //Damage = 2 + (1 * (Level - 1));
        //weapon.Attacks[0].Damage = (weapon.Attacks[0].Damage + (1 * (Level - 1)));
        data.CurrentHealth = data.MaxHealth;
    }

    public void SetInvulnerable(bool value)
    {
        invulnerable = value;
    }

    
    
}
