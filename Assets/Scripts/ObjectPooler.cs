using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	[System.Serializable]
	public class Pool 
	{
		public string tag;
		public GameObject prefab;
		public int size;
		public Transform parent;
	}

	public static ObjectPooler Instance;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		if(Instance != this && Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
		}

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                if (pool.parent != null)
                    obj.transform.SetParent(pool.parent);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
		
	}

	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	// Use this for initialization
	void Start () {
		
	}

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if(!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag " + tag + " doesn't exist");
			return null;
		}



		GameObject objToSpawn = poolDictionary[tag].Dequeue();
		objToSpawn.SetActive(true);
		objToSpawn.transform.position = position;
		objToSpawn.transform.rotation = rotation;

		//TODO: Re-enable if I use this 
		/*IPoolable pooledObject = objToSpawn.GetComponent<IPoolable>();

		if(pooledObject != null)
		{
			pooledObject.OnObjectSpawn();
		}*/


		poolDictionary[tag].Enqueue(objToSpawn);

		return objToSpawn;
	}

}
