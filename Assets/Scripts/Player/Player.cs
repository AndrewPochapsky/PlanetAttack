using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingCreature {

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
        RequiredXP = 100 + (50 * (Level - 1));
        CurrentHealth = 10;
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

    protected void SetRotation(float rotation, Transform target)
    {
        print("setting rotation");
        RotationVector.y = rotation;
        

        //target.eulerAngles = new Vector3(0, rotation, transform.rotation.z); 

    }
    protected void SetZRotation(Transform target)
    {
        RotationVector.z = transform.rotation.z;
        target.rotation = Quaternion.Euler(RotationVector);
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


}
