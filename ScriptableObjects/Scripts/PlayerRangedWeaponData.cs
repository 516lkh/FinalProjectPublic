using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRangedWeaponData
{
    [field: Header("Player Range Weapon Data")]

    [field: SerializeField][field: Range(1f, 1000f)] public float ProjectileDamage;

    [field: SerializeField][field: Range(0f, 25f)] public int ProjectileNumber;

    [field: SerializeField][field: Range(0f, 1000f)] public float ProjectileSize;

    [field: SerializeField][field: Range(1f, 1000f)] public float ProjectileSpeed;

    [field: SerializeField][field: Range(1f, 1000f)] public int ProjectilePiercing;

    [field: SerializeField][field: Range(1f, 1000f)] public float ShootRange;

    [field: SerializeField][field: Range(0f, 100f)] public int BulletCapacity;

    [field: SerializeField][field: Range(0f, 1000f)] public float FireRatePerMinute;

    [field: SerializeField][field: Range(0f, 100f)] public float ReloadingTime;

    [field: SerializeField][field: Range(0f, 100f)] public float MinuteOfAngleMin;
    [field: SerializeField][field: Range(0f, 100f)] public float MinuteOfAngleMax;
    [field: SerializeField][field: Range(0f, 100f)] public float MinuteOfAngleRecoveryPerSec;
    [field: SerializeField][field: Range(0f, 100f)] public float MinuteOfAngleIncreasePerFire;

    [field: SerializeField][field: Range(0f, 100f)] public float HorizontalRecoil;
    [field: SerializeField][field: Range(0f, 100f)] public float VerticalRecoil;  
    
    [field: SerializeField][field: Range(0f, 1f)] public float CriticalDamageChance;   
    [field: SerializeField][field: Range(0f, 10f)] public float CriticalDamageMultiplier;   
    

    [field: SerializeField] public AudioClip FireSoundEffect;
    [field: SerializeField] public AudioClip ReloadSoundEffect;
    


}
