using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState {INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerCoordinate;
    [SerializeField] private Transform enemyCoordinate;

    [SerializeField] private HpHud playerHUD;
    [SerializeField] private HpHud enemyHUD;


    Unit playerUnit;
    Unit enemyUnit;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject battleHUD;



    public BattleState state;

    void Start()
    {
        battleHUD.SetActive(false);
        state = BattleState.INACTIVE;
        //StartCoroutine(SetupBattle());
    }

   IEnumerator SetupBattle()
    {
        battleHUD.SetActive(true);
       
        GameObject playerAct =  Instantiate(playerPrefab, playerCoordinate);
        playerUnit = playerAct.GetComponent<Unit>();

        GameObject enemyAct = Instantiate(enemyPrefab, enemyCoordinate);
        enemyUnit = enemyAct.GetComponent<Unit>();

        dialogueText.text = $"{enemyUnit.unitName} appears!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You hit the enemy!";

        //call hurt function for hit enemy

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            //EndBattle();
        }
        else
        {
            //return enemy back to idle
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        //Change State
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerUnit.healDamage);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You heal yourself!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        if (enemyUnit.currentHP < 9)
        {
            int crit = Random.Range(0, 4);
            if (crit == 4)
            {
                dialogueText.text = $"{enemyUnit.name} uses a draining attack!";
                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.ultraDamage);
                enemyUnit.Heal(enemyUnit.healDamage);

                playerHUD.SetHP(playerUnit.currentHP);
                enemyHUD.SetHP(enemyUnit.currentHP);

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    EndBattle();
                }
                else
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
            else
            {
                dialogueText.text = $"{enemyUnit.name} attacks!";
                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


                playerHUD.SetHP(playerUnit.currentHP);

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    EndBattle();
                }
                else
                {
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
        }

        else
        {
            dialogueText.text = $"{enemyUnit.name} attacks!";
            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You win! Yay!";
        } else if(state == BattleState.LOST)
        {
            dialogueText.text = "You lost...";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";

    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
}
