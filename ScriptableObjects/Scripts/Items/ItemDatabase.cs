using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public ItemData[] CommonitemData;
    public ItemData[] UncommonitemData;
    public ItemData[] UniqueitemData;
}
