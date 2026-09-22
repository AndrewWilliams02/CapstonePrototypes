using UnityEngine;

public class Player : MonoBehaviour
{
    public ConstraintsManager cm;
    public Enemy enemy;
    public SkillTest[] skills;
    int[] skillCooldowns = new int[4];
    int[] skillUses = new int[4];
    public float health;
    public int maxEnergy;
    int energy = 0;

    private void Start()
    {
        skillCooldowns[0] = 0;
        skillCooldowns[1] = 0;
        skillCooldowns[2] = 0;
        skillCooldowns[3] = 0;

        skillUses[0] = skills[0].maxUses;
        skillUses[1] = skills[1].maxUses;
        skillUses[2] = skills[2].maxUses;
        skillUses[3] = skills[3].maxUses;
    }

    public void TakeDamage(int damage)
    {

    }

    public void UseSkill(int skillIndex)
    {
        if (cm.maxUsesEnabled)
        {
            if (skillUses[skillIndex] <= 0)
            {
                Debug.Log("No more uses!");
                return;
            }
            else
            {
                skillUses[skillIndex]--;
            }
        }
        if (cm.energyCostEnabled)
        {
            if (skills[skillIndex].cost > energy)
            {
                Debug.Log("Not enough energy!");
                return;
            }
            else
            {
                energy -= skills[skillIndex].cost;
            }
        }
        if (cm.cooldownsEnabled)
        {
            skillCooldowns[skillIndex] = skills[skillIndex].turnCooldown;
        }
        if (cm.elementsEnabled)
        {
            enemy.TypeCheck(skills[skillIndex].skillType);
        }

        enemy.TakeDamage(skills[skillIndex].damage);
    }
}
