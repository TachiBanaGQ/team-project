using Unity.VisualScripting;
using UnityEngine;

public class Enemymove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] GameObject enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int facingDirection = -1;

    [SerializeField] private EnemyState enemyState, newState;
   
    [SerializeField] Vector2 _direction;

    [SerializeField] float ChaseSpeed;

    [SerializeField] float AggroDist;

    [SerializeField] float StopDistance;
    [SerializeField] float ReturnDistance;

    [SerializeField] bool Flee = false;

    
    [SerializeField] private BattleManager bm;
    [SerializeField] private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //don't destroy on load function
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }

    // Update is called once per frame
    void Update()
    {
        

        if (enemyState != newState)
        {
            
            ChangeState(newState);
            UpdateAnimatior();
            Debug.Log(enemyState.ToString());
        }


        if (Player.position.x > transform.position.x && facingDirection == -1 || Player.position.x < transform.position.x && facingDirection == 1)
        {

            Flip();
        }
        Vector2 direction = (Player.position - transform.position).normalized;
        rb.linearVelocity = direction;


        float distance = Vector2.Distance(transform.position, Player.position);


        if (distance > AggroDist || distance <= StopDistance) return;

        if (!Flee)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, ChaseSpeed * Time.deltaTime);

        }
        else
        {
            if (distance > ReturnDistance) Flee = false;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -1 * ChaseSpeed * Time.deltaTime);
        }



    }





    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    { 

        Debug.Log("Trying to on trigger enter");
        if (collision.CompareTag("Player"))
        {
            Player = collision.transform;
            Debug.Log("load");
            newState = EnemyState.Moving;
            
        }

        if (collision.CompareTag("Encounter"))
        {
            bm.LaunchBattle(enemy, collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            rb.linearVelocity = Vector2.zero;
            newState = EnemyState.Idle;
           
        }
    }

    /// <summary>
    /// Getter
    /// </summary>
    /// <returns></returns>
    public EnemyState CurrentState()
    {
        return enemyState;
    }

    /// <summary>
    /// Setter
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
    }
    public void UpdateAnimatior()
    {
        //Exit the current animation
        switch (enemyState)
        {

            case EnemyState.Moving:
                anim.SetBool("IsMoving", true);
                break;
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
            anim.SetBool("IsMoving", false);
            anim.SetBool("IsHitting", false);
            anim.SetBool("IsHurting", false);
            anim.SetBool("IsIdle", true);

        }

        Debug.Log("checking to see if this works in the update loop");
    }




    //Update our current state


    //Update the new animation




}


public enum EnemyState
{
    Idle, Moving, Hurting, Hitting
}
