using UnityEngine;
public enum SkillName { Tornado, Poison, Stealth }

[CreateAssetMenu(fileName = "EnemySkillSO", menuName = "Characters/Enemy/Skill")]
public class EnemySkillDataSO : ScriptableObject
{
    public SkillName skillName;
    public float enemySkillInterval;
}
