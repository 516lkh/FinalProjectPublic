using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillPosition
{
    Player,
    Weapon,
    None,
}

public enum WeaponType
{
    Ripple,
    RangedWeapon,
    ClosedRangedWeapon,
    None,
}

public enum SkillRangeType
{
    RangedAttack,
    ClosedRangeAttack,
    None,
}

public enum SkillType
{
    Attack,
    Defense,
    Heal,
    Util,
    None,
}

[CreateAssetMenu(fileName = "SkillSO", menuName = "New Skill")]
public class SkillSO : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public Sprite icon;
    public int unlockLv;
    public float skillTime;
    public float coolTime;
    public GameObject skillEffect;
    public SkillPosition skillPosition;
    public SkillRangeType skillRangeType;
    public SkillType skillType;
    public WeaponType weaponType;

    [Header("Weapon Effect Value")]
    public float projectileDamage;
    public float projectileNum;
    public float projectileSpeed;
    public float shootRange;
    public float bulletCapacity;
    public float fireRatePerMinute;
    public float reloadingTime;
    public float minuteOfAngleMin;
    public float minuteOfAngleRecoveryPerSec;
    public float minuteOfAngleIncreasePerFire;
    public float recoil;
    public float effectNum;
    public float effectTime;
    public float effectHpPercentage;

    [Header("Player Effect Value")]
    public float playerMovementSpeed;
    public float playerJumpForce;
    public float playerstartMaxHealthPoint;
    public float playerHealthPointRecovery;
    public float playerStaminaPointRecovery;
    public float playerStaminaPointconsumption;
}
