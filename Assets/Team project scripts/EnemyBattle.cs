using UnityEngine;

public class EnemyBattle : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] private EnemyState enemyState, newState;
    [SerializeField] private Animator anim;


    void Start()
    {
        //don't destroy on load function
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }

    void Update()
    {
        if (enemyState != newState)
        {

            ChangeState(newState);
            UpdateAnimatior();
            Debug.Log(enemyState.ToString());
        }
    }

    public EnemyState CurrentState()
    {
        return enemyState;
    }

    public void UpdateAnimatior()
    {
        //Exit the current animation
        switch (enemyState)
        {

            
            case EnemyState.Hurting:
                anim.SetBool("IsHurting", true);

                break;
            case EnemyState.Hitting:
                anim.SetBool("IsHitting", true);
                break;

        }

        if (enemyState != EnemyState.Idle)
        {

            anim.SetBool("IsIdle", false);
        }
        else
        {
          
            anim.SetBool("IsHitting", false);
            anim.SetBool("IsHurting", false);
            anim.SetBool("IsIdle", true);

        }

        Debug.Log("checking to see if this works in the update loop");
    }

    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
    }
}



