using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamp : MonoBehaviour {
    [SerializeField]
    private Enemy[] enemyTypes;
    [SerializeField]
    private int amountToSpawn;
    [SerializeField][Tooltip("Spawn ever x amount of seconds")]
    private float spawnSpeed;
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerator SpawnEnemies()
    {
        int randomEnemyIndex = Random.Range(0, enemyTypes.Length);
        Enemy enemyToSpawn = enemyTypes[randomEnemyIndex];
        for (int i = 0; i < amountToSpawn; i++)
        {
            yield return new WaitForSeconds(spawnSpeed);
            Instantiate(enemyToSpawn, transform.position, transform.rotation);
        }
        print("SPAWNING");
    }

}
