using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public ConstraintsManager cm;
    public Player player;
    public Enemy enemy;
    public GameObject battleUI;
    public GameObject[] cooldownsUI, energyCostUI, maxUsesUI;

    public bool isPlayerTurn = true;
    public bool battleUIEnabled = true;

    private void Start()
    {
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
        for (int i = 0; i < cooldownsUI.Length; i++)
        {
            cooldownsUI[i].SetActive(cm.cooldownsEnabled);
        }

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
}
