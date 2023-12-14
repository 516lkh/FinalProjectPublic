using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillDatabaseSO", menuName = "New SkillDatabase")]
public class SkillDatabaseSO : ScriptableObject
{
    public SkillSO[] playerSkillDatabase;
    public SkillSO[] weaponSkillDatabase;
    public SkillSO[] ultimateSkillDatabase;
}
