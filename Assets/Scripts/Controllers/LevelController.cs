using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    //Easier to just always pass in the required score than to make it an int? type 
    public delegate void OnScoreUpdated(int currentScore, int requiredScore);
    public event OnScoreUpdated OnScoreUpdatedEvent;

    public static LevelController Instance;

    public int CurrentScore { get; private set; }
    int requiredScore;

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
    }

    // Use this for initialization
    void Start () {
        LevelData data = Resources.Load<LevelData>("ScriptableObjects/LevelData");
        requiredScore = data.levelScoreRequirements[data.currentLevel - 1];

        OnScoreUpdatedEvent(CurrentScore, requiredScore);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScore(int amount)
    {
        if(CurrentScore <= requiredScore)
        {
            CurrentScore += amount;
            if(CurrentScore >= requiredScore)
            {
                CurrentScore = requiredScore;
            }
        }
            
        OnScoreUpdatedEvent(CurrentScore, requiredScore);

        if (CurrentScore >= requiredScore && !Portal.Instance.Activated)
        {
            Portal.Instance.Activate();
        }
    } 
}
