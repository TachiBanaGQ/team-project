using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR;


public enum BattleState { INACTIVE, START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerCoordinate;
    [SerializeField] private Transform enemyCoordinate;

    [SerializeField] private HpHud playerHUD;
    [SerializeField] private HpHud enemyHUD;

    Enemymove enemymove;
    PlayerAction playeraction;

    private GameObject playerGO;
    private GameObject enemyGO;
    private GameObject playerAct;
    private GameObject enemyAct;


    Unit playerUnit;
    Unit enemyUnit;

    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject battleHUD;
    [SerializeField] private GameObject atkButton;
    [SerializeField] private GameObject healButton;

    [SerializeField] public string _currentState;
    [SerializeField] private EnemyState enemyState, newState;
    public BattleState state;

    void Start()
    {
        battleHUD.SetActive(false);
        state = BattleState.INACTIVE;

        _currentState = "Idle";
        ChangeState(EnemyState.Idle);
        enemymove = GetComponent<Enemymove>();
        playeraction = GetComponent<PlayerAction>();
    }

    private void Update()
    {
        if (enemyState != newState)
        {

            ChangeState(newState);
            Debug.Log(enemyState.ToString());
        }
    }

    public void LaunchBattle(GameObject player, GameObject enemy)
    {
        playerGO = player;
        playerGO.SetActive(false);
        enemyGO = enemy;
        enemyGO.SetActive(false);
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        battleHUD.SetActive(true);

        playerAct = Instantiate(playerPrefab, playerCoordinate);
        playerUnit = playerAct.GetComponent<Unit>();

        enemyAct = Instantiate(enemyPrefab, enemyCoordinate);
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

        //call player attack anim
            _currentState = "Hitting";

        
     
        //call enemy hurt anim
        newState = EnemyState.Hurting;
        //if(enemy health <-1){ newState = EnemyState.Hurting;) }

        yield return new WaitForSeconds(2f);

        //call player idle anim
        _currentState = "Idle";

        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        }
        else
        {
            //call enemy idle anim
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

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
                dialogueText.text = $"{enemyUnit.unitName} uses a draining attack!";
                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.ultraDamage);
                enemyUnit.Heal(enemyUnit.healDamage);

                playerHUD.SetHP(playerUnit.currentHP);
                enemyHUD.SetHP(enemyUnit.currentHP);

                //call enemy attack anim
                newState = EnemyState.Hitting;
                //call player hurt anim
                _currentState = "hurting";

                yield return new WaitForSeconds(1f);

                //call enemy idle anim
                newState = EnemyState.Idle;

                if (isDead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                }
                else
                {
                    //call player idle anim
                    _currentState = "Idle";
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
            else
            {
                dialogueText.text = $"{enemyUnit.unitName} attacks!";
                yield return new WaitForSeconds(1f);

                bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

                playerHUD.SetHP(playerUnit.currentHP);

                //call enemy attack anim
                newState = EnemyState.Hitting;
                //call player hurt anim
                _currentState = "Hurting";

                yield return new WaitForSeconds(1f);

                if (isDead)
                {
                    state = BattleState.LOST;
                    StartCoroutine(EndBattle());
                }
                else
                {
                    //call player idle anim
                    _currentState = "Idle";
                    
                    state = BattleState.PLAYERTURN;
                    PlayerTurn();
                }
            }
        }

        else
        {
            dialogueText.text = $"{enemyUnit.unitName} attacks!";
            yield return new WaitForSeconds(1f);

            bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

            //call enemy attack anim
            newState = EnemyState.Hitting;

            //call player hurt anim
            _currentState = "Hurting";

            playerHUD.SetHP(playerUnit.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                StartCoroutine(EndBattle());
            }
            else
            {
                //call player idle anim
                _currentState = "Idle";
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
    }

    IEnumerator EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You win! Yay!";
            yield return new WaitForSeconds(5f);

            Object.Destroy(playerAct);
            Object.Destroy(enemyAct);
            Object.Destroy(enemyGO);

            playerGO.SetActive(true);

            battleHUD.SetActive(false);


        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You lost...";
            yield return new WaitForSeconds(10f);
            //scene change main menu
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
        IsButtonsActive(true);
    }
    void IsButtonsActive(bool state)
    {
        if (state == true)
        {
            atkButton.SetActive(true);
            healButton.SetActive(true);

        }
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

    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
    }
}
