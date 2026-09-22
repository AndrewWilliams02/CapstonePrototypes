using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTest", menuName = "Scriptable Objects/EnemyTest")]
public class EnemyTest : ScriptableObject
{
    public string enemyName;
    public float health;
    public Vector2 damage;
    public TestTypes enemyType;
}
