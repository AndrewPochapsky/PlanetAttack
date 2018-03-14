using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public delegate void OnHealthUpdated(int curr, int max);
    public event OnHealthUpdated OnHealthUpdatedEvent;

    public delegate void OnCoinsUpdated(int value);
    public event OnCoinsUpdated OnCoinsUpdatedEvent;

    public int Coins { get; protected set; }

    public AudioClip levelUpClip, hitClip;
    
    private AudioSource audioSource;

    private float invulnerabilityTimer = 0.5f;
    private bool invulnerable = false;
    public Transform hand;

    public static Player Instance;
    
    protected override void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        base.Awake();

        //data = new EntityData();

        data.Level = 1;
        data.CurrentXP = 0;
        data.RequiredXP = 100 + (25 * (data.Level - 1));
        data.CurrentHealth = 12;
        data.MaxHealth = data.CurrentHealth;
        data.Speed = 5;
        data.JumpStrength = 15;
        DG = transform.GetChild(0).GetComponent<DetectGround>();
        audioSource = GetComponent<AudioSource>();

        EquipWeapon("Pistol");
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        OnHealthUpdatedEvent(data.CurrentHealth, data.MaxHealth);
        OnCoinsUpdatedEvent(Coins);
    }

    public override void RecieveDamage(int damage)
    {
        base.RecieveDamage(damage);

        OnHealthUpdatedEvent(data.CurrentHealth, data.MaxHealth);

        audioSource.clip = hitClip;
        audioSource.Play();

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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collectible collectible = collision.GetComponent<Collectible>();
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interactable.Interact();
        }
        /*if (collectible && !GetComponent<RangedWeapon>())
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
        }*/
       
    }



    protected override void Die()
    {
        DifficultyController.SurvivedTime = Time.timeSinceLevelLoad.ToString("F2");
        LevelManager.Instance.LoadLevel("_02End");
        base.Die();
    }
    protected override void LevelUp()
    {
        base.LevelUp();
        audioSource.clip = levelUpClip;
        audioSource.Play();
        //StartCoroutine(controller.DisplayLevelUpText());

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

    public void IncrementCoins(int value)
    {
        Coins += value;
        OnCoinsUpdatedEvent(Coins);
    }

    /// <summary>
    /// Equips the weapon of 'name' by getting it from WeaponManager
    /// </summary>
    /// <param name="name">The weapon to equip</param>
    public void EquipWeapon(string name)
    {
        RangedWeapon weapon = WeaponManager.Instance.GetWeapon(name);
        if(weapon != null)
        {
            GameObject currentWeapon = null;
            if(hand.transform.childCount > 0)
            {
                currentWeapon = hand.GetChild(0).gameObject;
            }

            if(currentWeapon != null)
            {
                currentWeapon.transform.SetParent(WeaponManager.Instance.transform, false);
                currentWeapon.SetActive(false);
            }
            weapon.gameObject.SetActive(true);
            weapon.transform.SetParent(hand, false);
        }
        else
        {
            Debug.LogWarning("Weapon: " + name + " can not be found");//
        }
    }    
}
