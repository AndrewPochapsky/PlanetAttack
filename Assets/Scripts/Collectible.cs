using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    protected int Value { get; set; }
    protected AudioSource audioSource;
    protected SpriteRenderer sp;
    protected CircleCollider2D col;
    protected virtual void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        col = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public int GetValue()
    {
        return Value;
    }

    public void Destroy()
    {
        audioSource.Play();
        print("playing");
        sp.enabled = false;
        col.enabled = false;
        StartCoroutine(Remove());


        
    }


    private IEnumerator Remove()
    {
        yield return new WaitForSeconds(3);
        if (transform.parent != null)
        {


            Transform parent = transform.parent;
            Destroy(parent.gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }
   

}
