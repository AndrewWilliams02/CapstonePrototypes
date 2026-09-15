using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DataList : ScriptableObject
{
    public List<Enemy> allEnemies = new List<Enemy>();
    public List<Skills> allSkills = new List<Skills>();
}