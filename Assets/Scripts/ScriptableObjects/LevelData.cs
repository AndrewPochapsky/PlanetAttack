﻿using UnityEngine;

[CreateAssetMenu(fileName = "LevelData")]
public class LevelData : ScriptableObject {
    public int currentLevel;
    public int[] levelScoreRequirements;
}
