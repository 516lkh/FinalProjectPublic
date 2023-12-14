using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct BoxData
{
    public Vector3 Positions;
    public Vector3 Rotations;
}
[CreateAssetMenu(menuName = "ItemBoxPositions")]
public class ItemBoxPositions : ScriptableObject
{
    public BoxData[] boxDatas;
}
