using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Player player;

    public Vector2 damage;
    public TestTypes enemyType = TestTypes.Red;

    float damageMod = 1;

    public SpriteRenderer sr;

    float health = 50;
    public Slider healthBar;


    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ResetState();
    }

    public void ChangeEnemyType(TestTypes type)
    {
        enemyType = type;
    }

    public void AttackPlayer()
    {
        float finalDamage = Mathf.Round(Random.Range(damage.x, damage.y) * 10f) / 10f;
        player.TakeDamage(finalDamage);
    }

    void TypeCheck(TestTypes attackType)
    {
        if (attackType == TestTypes.Red)
        {
            if (enemyType == TestTypes.Red)
            {
                damageMod = 1;
                Debug.Log("Attack is Neutral");
            }
            else if (enemyType == TestTypes.Green)
            {
                damageMod = 2;
                Debug.Log("Attack is Effective");
            }
            else if (enemyType == TestTypes.Blue)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Resisted");
            }
        }
        else if (attackType == TestTypes.Green)
        {
            if (enemyType == TestTypes.Red)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Resisted");
            }
            else if (enemyType == TestTypes.Green)
            {
                damageMod = 1f;
                Debug.Log("Attack is Neutral");
            }
            else if (enemyType == TestTypes.Blue)
            {
                damageMod = 2f;
                Debug.Log("Attack is Effective");
            }
        }
        else if (attackType == TestTypes.Blue)
        {
            if (enemyType == TestTypes.Red)
            {
                damageMod = 2;
                Debug.Log("Attack is Effective");
            }
            else if (enemyType == TestTypes.Green)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Resisted");
            }
            else if (enemyType == TestTypes.Blue)
            {
                damageMod = 1;
                Debug.Log("Attack is Neutral");
            }
        }
    }

    public void TakeDamage(SkillTest skill, bool elementEnabled)
    {
        if(elementEnabled)
        {
            TypeCheck(skill.skillType);
        }

        float finalDamage = skill.damage * damageMod;
        health -= finalDamage;
        healthBar.value = health;
        Debug.Log($"Enemy took {finalDamage}");

        damageMod = 1;
    }

    public void ResetState()
    {
        damageMod = 1;
        health = 50;
        healthBar.value = health;
        healthBar.maxValue = health;
    }
}
