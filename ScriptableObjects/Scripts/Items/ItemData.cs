using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemGrade
{
    Common,
    Uncommon,
    Unique
}

[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemGrade grade;
    public GameObject dropPrefab;
    public Sprite icon;
    public float weight;
}
