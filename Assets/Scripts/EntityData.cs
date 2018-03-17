using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EntityData {
    public string Name;
    [HideInInspector]
    public int CurrentHealth;
    public int MaxHealth;
    public float Speed;
    public float JumpStrength;
    public int Level;
    public int CurrentXP;
    public int RequiredXP;
    
    /* Enemy Specific */
    public int Damage;
    public int MinCoins;
    public int MaxCoins;
}
