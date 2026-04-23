using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class Turnstatemachine : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerCoordinate;
    [SerializeField] private Transform enemyCoordinate;

    [SerializeField] private HpHud playerHUD;
    [SerializeField] private HpHud enemyHUD;


    Unit playerUnit;
    Unit enemyUnit;

    [SerializeField] private Text dialogueText;

   public BattleState state;

    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

   void SetupBattle()
    {
        
       GameObject playerAct =  Instantiate(playerPrefab, playerCoordinate);
        playerUnit = playerAct.GetComponent<Unit>();

        GameObject enemyAct = Instantiate(enemyPrefab, enemyCoordinate);
        enemyUnit = playerAct.GetComponent<Unit>();

        dialogueText.text = enemyUnit.unitName;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
    }
}
