using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemymove : MonoBehaviour
{
    [SerializeField] public Transform Player;
    [SerializeField] float ChaseSpeed;

    [SerializeField] float AggroDist;

    [SerializeField] float StopDistance;
    [SerializeField] float ReturnDistance;

    [SerializeField] bool Flee = false;

    [SerializeField] private float gridSize = 1f;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float moveDuration = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Collider>().gameObject.tag == "Player")
        {
            SceneManager.LoadScene("BattleScene");
            Debug.Log("load");
            Flee = true;
        }
    }
}
