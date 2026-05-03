using UnityEngine;

public class EnemyTestscript : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rb;
    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * Speed;
    }
}
