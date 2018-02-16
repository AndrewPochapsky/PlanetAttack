using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour {

    public static string SurvivedTime { get; set; }

    public static int IncrementTime { get; private set; } = 20;

    public static int CurrentXP { get; private set; }
    public const int XPRate = 2;

	// Use this for initialization
	void Start () {
        CurrentXP = 0;
        InvokeRepeating("IncrementXP", 0, 1);
	}

    public static void AddXP(int xp)
    {
        CurrentXP += xp;
    }

    private void IncrementXP()
    {
        AddXP(XPRate);
    }
}
