using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public ConstraintsManager cm;
    public Player player;
    public Enemy enemy;
    public GameObject battleUI;
    public GameObject[] cooldownsUI, energyCostUI, maxUsesUI;
    TMP_Dropdown enemyType;

    public bool isPlayerTurn = true;
    public bool battleUIEnabled = true;

    private void Start()
    {
        enemyType = cm.enemyType;
        SetUI();
    }

    private void Update()
    {
        if (isPlayerTurn)
        {
            if(!battleUIEnabled) { battleUI.SetActive(true); }
        }
        else
        {
            if (battleUIEnabled) { battleUI.SetActive(false); }
            EnemyTurn();
        }
    }

    void SetUI()
    {
        for (int i = 0; i < energyCostUI.Length; i++)
        {
            energyCostUI[i].SetActive(cm.energyCostEnabled);
        }

        for (int i = 0; i < maxUsesUI.Length; i++)
        {
            maxUsesUI[i].SetActive(cm.maxUsesEnabled);
        }
    }

    void EnemyTurn()
    {
        battleUIEnabled = false;
        enemy.AttackPlayer();
        isPlayerTurn = true;
    }

    public void ResetBattle()
    {
        SetUI();
        player.ResetState();
        enemy.ResetState();
        isPlayerTurn = true;
    }

    public void SetEnemyType()
    {
        enemy.enemyType = (TestTypes)enemyType.value;

        if(enemy.enemyType == TestTypes.Red)
        {
            enemy.sr.color = Color.red;
        }
        else if (enemy.enemyType == TestTypes.Green)
        {
            enemy.sr.color = Color.green;
        }
        else if (enemy.enemyType == TestTypes.Blue)
        {
            enemy.sr.color = Color.blue;
        }
    }
}
