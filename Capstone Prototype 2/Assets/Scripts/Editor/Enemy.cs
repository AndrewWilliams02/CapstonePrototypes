using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    public string enemyName;
    public float health;
    public float armor;
    public float damage;
    public float attackInterval;
}
