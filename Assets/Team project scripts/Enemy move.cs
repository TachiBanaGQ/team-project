using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemymove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int facingDirection = -1;

    [SerializeField] private EnemyState enemyState, newState;

    [SerializeField] float ChaseSpeed;

    [SerializeField] float AggroDist;

    [SerializeField] float StopDistance;
    [SerializeField] float ReturnDistance;

    [SerializeField] bool Flee = false;

    [SerializeField] private float gridSize = 1f;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float moveDuration = 1.0f;

    [SerializeField] private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.Moving)
        {
            if (Player.position.x > transform.position.x && facingDirection == -1 || Player.position.x < transform.position.x && facingDirection == 1)
            {

                Flip();
            }
            Vector2 direction = (Player.position - transform.position).normalized;
            rb.linearVelocity = direction * gridSize;


        }

        float distance = Vector2.Distance(transform.position, Player.position);


        if (distance > AggroDist || distance <= StopDistance) return;

        if (!Flee)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, ChaseSpeed * Time.deltaTime * gridSize);
        }
        else
        {
            if (distance > ReturnDistance) Flee = false;
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -1 * ChaseSpeed * Time.deltaTime * gridSize);
        }

        

    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator Move(Vector2 direction)
    {

        //Make a note of where we are and where we are going.
        isMoving = true;

        //Make a note of where we are and whewre we are going.
        Vector2 startPosition = transform.position;
        Vector2 endPosition = startPosition + (direction * gridSize);

        //Smoothly move in the desired direction taking the required time.
        float elapsedTime = 0;
        while (elapsedTime < Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / moveDuration;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent);
            yield return null;
        }

        //Make sure we end up excactly where we want.
        transform.position = endPosition;

        //We're no longer moving so we can accept another move input.
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<Collision>().gameObject.tag == "Player")
        {
            SceneManager.LoadScene("BattleScene");
            Debug.Log("load");
            if (Player == null)
            {
                Player = collision.transform;

            }
            ChangeState(EnemyState.Moving);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (GetComponent<Collision>().gameObject.tag == "Player")
        {

            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        //Exit the current animation
        if (enemyState == EnemyState.Idle)

            anim.SetBool("IsIdle", false);

        else if (enemyState == EnemyState.Moving)

            anim.SetBool("IsMoving", false);


        //Update our current state
        enemyState = newState;

        //Update the new animation
        if (enemyState == EnemyState.Idle)

            anim.SetBool("IsIdle", true);

        else if (enemyState == EnemyState.Moving)
            anim.SetBool("IsMoving", true);

    }
}

public enum EnemyState
{
    Idle, Moving, Hurting, Hitting
}
