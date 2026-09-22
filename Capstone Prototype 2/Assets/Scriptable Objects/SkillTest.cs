using UnityEngine;

[CreateAssetMenu(fileName = "SkillTest", menuName = "Scriptable Objects/SkillTest")]
public class SkillTest : ScriptableObject
{
    public string skillName;
    public float damage;
    public int cost;
    public int maxUses;
    public int turnCooldown;
    public TestTypes skillType;
}
