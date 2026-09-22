using UnityEngine;

public class Player : MonoBehaviour
{
    public TurnManager tm;
    public ConstraintsManager cm;
    public Enemy enemy;
    public SkillTest[] skills;
    int[] skillCooldowns = new int[4];
    int[] skillUses = new int[4];
    public int maxEnergy;
    int energy = 0;

    public SpriteRenderer[] energyIcons;

    private void Start()
    {
        ResetState();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"Player took {damage}");
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
                SetEnergyUI();
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
        EndTurn();
    }

    void EndTurn()
    {
        if(cm.energyCostEnabled && (energy < maxEnergy))
        {
            energy++;
            SetEnergyUI();
        }
        tm.isPlayerTurn = false;
    }

    void SetEnergyUI()
    {
        for (int i = 0; i < energyIcons.Length; i++)
        {
            if ((i+1) == energy)
            {
                energyIcons[i].color = Color.blue;
            }
            else
            {
                energyIcons[i].color = Color.grey;
            }
        }
    }

    public void ResetState()
    {
        energy = 0;
        SetEnergyUI();
        for (int i = 0; i < skillCooldowns.Length; i++)
        {
            skillCooldowns[i] = 0;
        }
        for (int i = 0; i < skillUses.Length; i++)
        {
            skillUses[i] = skills[i].maxUses;
        }
    }
}
