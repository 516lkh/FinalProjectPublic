using UnityEngine;

public enum EnemyType { LowLevel, MiddleLevel, Boss }

[CreateAssetMenu(fileName = "EnemySO", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; private set; } = 100;
    [field: SerializeField] public float ChasingRange { get; private set; } = 10f;
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float WalkSpeed { get; private set; } = 1;
    [field: SerializeField] public float RunSpeed { get; private set; } = 5;
    [field: SerializeField][field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField] public float minWanderDistance { get; private set; } = 0.1f;
    [field: SerializeField] public float maxWanderDistance { get; private set; } = 20f;
    [field: SerializeField] public EnemyType Type { get; private set; }
    [field: SerializeField] public int Exp { get; private set; }
}