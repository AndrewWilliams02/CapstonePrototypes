using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstraintsManager : MonoBehaviour
{
    public bool maxUsesEnabled = true;
    public bool energyCostEnabled = true;
    public bool elementsEnabled = true;
    public bool cooldownsEnabled = true;

    public Toggle maxUses, energy, elements, cooldowns;
    public TMP_Dropdown enemyType;

    private void Start()
    {
        PopulateList();
    }

    public void ApplyChanges()
    {
        maxUsesEnabled = maxUses.isOn;
        energyCostEnabled = energy.isOn;
        elementsEnabled = elements.isOn;
        cooldownsEnabled = cooldowns.isOn;
    }

    void PopulateList()
    {
        string[] enumNames = Enum.GetNames(typeof(TestTypes));
        List<string> names = new List<string>(enumNames);

        enemyType.AddOptions(names);
    }
}