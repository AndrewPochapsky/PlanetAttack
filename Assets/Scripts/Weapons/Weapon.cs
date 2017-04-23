using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public List<Attack> Attacks { get; set; }
    public Player player { get; set; }

    Enemy enemyTarget;

    private LivingCreature target;
    [HideInInspector]
    public Animator anim;
    // Use this for initialization
    protected virtual void Start () {
        Attacks = new List<Attack>();
        player = GameObject.FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
	}

    private void Update()
    {
        if (GetComponent<Player>())
        {
            print("weapon has player");
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        //for some reason this reads col as player so i need to go down and find the weapon through this
        //child thing
        target = col.gameObject.GetComponent<LivingCreature>();
        if (target)
        {
            
            Vector3 enemyPos = target.transform.position;
            if (target.GetComponent<Enemy>())
            {
                player.SetInvulnerable(true);
                print(target.ToString()+ " taking damage");
                
                target.RecieveDamage(player.GetLastAttack().GetDamage());
                //prevent enemy from stopping when no knockback
                if (player.GetLastAttack().GetKnockBack() > 0)
                {
                    KnockBack(target);
                }
            }
        }
        player.SetInvulnerable(false);
    }

    private void KnockBack(LivingCreature lc)
    {
        if (lc.GetComponent<Enemy>())
        {
            enemyTarget = lc.GetComponent<Enemy>();
        }
       
        Vector3 enemyPos = lc.transform.position;
        Vector2 weaponPos = transform.position;
        float xDifference = weaponPos.x - enemyPos.x;
        Rigidbody2D targetRB = lc.GetComponent<Rigidbody2D>();

        //player is on the left of enemy
        if (xDifference < 0)
        {
            targetRB.AddForce(Vector2.right * player.GetLastAttack().GetKnockBack(), ForceMode2D.Impulse);

            

           
        }
        //player is on the right of enemy
        else if (xDifference > 0)
        {

            targetRB.AddForce(Vector2.left * player.GetLastAttack().GetKnockBack(), ForceMode2D.Impulse);

            

            


        }
    }


}
