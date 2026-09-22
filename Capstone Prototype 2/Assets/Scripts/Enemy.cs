using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Player player;

    public float health;
    public Vector2 damage;
    public TestTypes enemyType = TestTypes.Red;

    float damageMod = 1;

    public void ChangeEnemyType(TestTypes type)
    {
        enemyType = type;
    }

    public void AttackPlayer()
    {
        
    }

    public void TypeCheck(TestTypes attackType)
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
                Debug.Log("Attack is Resisted");
            }
            else if (enemyType == TestTypes.Blue)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Effective");
            }
        }
        else if (attackType == TestTypes.Green)
        {
            if (enemyType == TestTypes.Red)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Neutral");
            }
            else if (enemyType == TestTypes.Green)
            {
                damageMod = 1f;
                Debug.Log("Attack is Resisted");
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
                Debug.Log("Attack is Neutral");
            }
            else if (enemyType == TestTypes.Green)
            {
                damageMod = 0.5f;
                Debug.Log("Attack is Resisted");
            }
            else if (enemyType == TestTypes.Blue)
            {
                damageMod = 1;
                Debug.Log("Attack is Effective");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage * damageMod;
        health -= finalDamage;
        Debug.Log($"Enemy took {finalDamage}");

        damageMod = 1;
    }
}
