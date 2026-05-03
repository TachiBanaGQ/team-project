using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Enemymove _enemyMove;

    private void Start()
    {
        _enemyMove = GetComponentInChildren<Enemymove>(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Triggered!");

            _enemyMove.ChangeState(EnemyState.Moving);
        }
    }
}

