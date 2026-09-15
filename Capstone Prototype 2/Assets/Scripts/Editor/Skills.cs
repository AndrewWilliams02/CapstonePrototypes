using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "Scriptable Objects/Skills")]
public class Skills : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public float damage;
    public int numOfHits;
    public float delay;
    public int cost;
}
