using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerConditionData
{
    [field: Header("Player Condition Data")]
    [field: SerializeField][field: Range(1f, 100f)] public int MaxLevel = 20;
    [field: SerializeField][field: Range(0f, 100f)] public float MovementSpeed = 5f;

    [field: SerializeField][field: Range(1f, 1000f)] public float StartMaxHealthPoint;
    [field: SerializeField][field: Range(0f, 1000f)] public float StartHealthPointRecovery;
    [field: SerializeField][field: Range(0f, 25f)] public float IncreaseOfHealthPointPerLevel;
    [field: SerializeField][field: Range(0f, 1000f)] public float IncreaseOfHealthPointRecoveryPerLevel;

    [field: SerializeField][field: Range(0f, 100f)] public float StartMaxStaminaPoint;
    [field: SerializeField][field: Range(0f, 25f)] public float StartStaminaPointRecovery;
    [field: SerializeField][field: Range(0f, 25f)] public float StaminaPointConsumption;
    [field: SerializeField][field: Range(0f, 25f)] public float IncreaseOfStaminaPointPerLevel;
    [field: SerializeField][field: Range(0f, 25f)] public float IncreaseOfStaminaPointRecoveryPerLevel;
    [field: SerializeField][field: Range(0f, 25f)] public float JumpForce { get; private set; } = 4f;

    [field: SerializeField] public AudioClip FootstepSoundEffect;
}
