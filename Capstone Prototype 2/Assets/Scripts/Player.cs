using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public TurnManager tm;
    public ConstraintsManager cm;
    public Enemy enemy;
    public SkillTest[] skills;
    int[] skillCooldowns = new int[3];
    int[] skillUses = new int[3];
    public int maxEnergy;
    int energy = 3;

    public Image[] energyIcons;
    public TextMeshProUGUI[] skillUsesText;
    public GameObject[] cooldowns;
    public TextMeshProUGUI[] cooldownText;
    public Button[] skillButtons;

    float health = 50;
    public Slider healthBar;

    private void Start()
    {
        ResetState();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log($"Player took {damage}");
        healthBar.value = health;
    }

    public void UseSkill(int skillIndex)
    {
        bool elementEnabled = cm.elementsEnabled;

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
                skillUsesText[skillIndex].text = $"Max Uses: {skillUses[skillIndex]}/{skills[skillIndex].maxUses}";
            }
        }
        if (cm.energyCostEnabled)
        {
            if (skills[skillIndex].cost > energy)
            {
                Debug.Log("Not enough energy!");
                if(cm.maxUsesEnabled)
                {
                    skillUses[skillIndex]++;
                    skillUsesText[skillIndex].text = $"Max Uses: {skillUses[skillIndex]}/{skills[skillIndex].maxUses}";
                }
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
            skillButtons[skillIndex].interactable = false;
            cooldowns[skillIndex].SetActive(true);
            cooldownText[skillIndex].text = $"{skillCooldowns[skillIndex]} Turns";
        }

        enemy.TakeDamage(skills[skillIndex], elementEnabled);
        EndTurn();
    }

    void UpdateCooldowns()
    {
        for (int i = 0; i < skillCooldowns.Length; i++)
        {
            if (skillCooldowns[i] > 0)
            {
                skillCooldowns[i]--;

                if (skillCooldowns[i] <= 0)
                {
                    cooldowns[i].SetActive(false);
                    cooldownText[i].text = "";
                    skillButtons[i].interactable = true;
                }
                else
                {
                    cooldownText[i].text = $"{skillCooldowns[i]} Turns";
                }
            }
        }
    }

    void EndTurn()
    {
        if(cm.energyCostEnabled && (energy < maxEnergy))
        {
            energy++;
            SetEnergyUI();
        }

        tm.isPlayerTurn = false;

        if (cm.cooldownsEnabled)
        {
            UpdateCooldowns();
        }
    }

    void SetEnergyUI()
    {
        for (int i = 0; i < energyIcons.Length; i++)
        {
            if ((i+1) <= energy)
            {
                energyIcons[i].color = Color.cyan;
            }
            else
            {
                energyIcons[i].color = Color.black;
            }
        }
    }

    public void ResetState()
    {
        health = 50;
        healthBar.value = health;
        healthBar.maxValue = health;

        energy = 3;
        SetEnergyUI();
        for (int i = 0; i < skillCooldowns.Length; i++)
        {
            skillCooldowns[i] = 0;
            cooldowns[i].SetActive(false); //Not working
            skillButtons[i].interactable = true;
        }
        for (int i = 0; i < skillUses.Length; i++)
        {
            skillUses[i] = skills[i].maxUses;
            skillUsesText[i].text = $"Max Uses: {skillUses[i]}/{skills[i].maxUses}";
        }
    }
}