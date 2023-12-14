using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerMeleeWeaponData
{
    [field: Header("Player Melee Weapon Data")]

    

    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas { get; private set; }
    public int GetAttackInfoCount() { return AttackInfoDatas.Count; }
    public AttackInfoData GetAttackInfo(int index) { return AttackInfoDatas[index]; }

    [field: SerializeField] public float MeleeWeaponAttackSpeed;
    [field: SerializeField] public int MeleeWeaponAttackDamage;
    [field: SerializeField] public float MeleeWeaponKnockBackForce;

    [field: SerializeField] public float MeleeWeaponUseStamina;

}
[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string AttackName { get; private set; }
    [field: SerializeField] public int ComboStateIndex { get; private set; }
    [field: SerializeField][field: Range(0f, 1f)] public float ComboTransitionTime { get; private set; }
    [field: SerializeField][field: Range(0f, 3f)] public float ForceTransitionTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }

    [field: SerializeField] public float DamageMultiplier { get; private set; }
    [field: SerializeField] public float MeleeWeaponKnockBackForceMultiplier { get; private set; }
}
