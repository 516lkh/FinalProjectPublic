using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]

public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerConditionData PlayerConditionData { get; private set; }
    [field: SerializeField] public PlayerRangedWeaponData PlayerRangedWeaponData { get; private set; }
    [field: SerializeField] public PlayerMeleeWeaponData PlayerMeleeWeaponData { get; private set; }

}
