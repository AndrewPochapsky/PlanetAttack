using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCamp : MonoBehaviour {

    [SerializeField]
    private string[] enemyTypes;

    [SerializeField]
    private int amountToSpawn;
    
    [SerializeField][Tooltip("Spawn every x amount of seconds")]
    private float spawnSpeed;

    private float timeBetweenSpawns = 6;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
        //StartCoroutine(IncrementSpawnAmount());
	}

    private IEnumerator SpawnEnemies()
    {
        int randomEnemyIndex = Random.Range(0, enemyTypes.Length);
        string enemyToSpawn = enemyTypes[randomEnemyIndex];

        for (int i = 0; i < amountToSpawn; i++)
        {
            ObjectPooler.Instance.SpawnFromPool(enemyToSpawn, transform.position, transform.rotation);
            yield return new WaitForSeconds(spawnSpeed);
        }
        print("SPAWNING");
        yield return new WaitForSeconds(timeBetweenSpawns);
        //StartCoroutine(SpawnEnemies());
    }

    private IEnumerator IncrementSpawnAmount()
    {
        yield return new WaitForSeconds(DifficultyController.IncrementTime);
       
        amountToSpawn++;
        print("incrementing amount, amount is now: " + amountToSpawn);
        StartCoroutine(IncrementSpawnAmount());

    }
    
}
