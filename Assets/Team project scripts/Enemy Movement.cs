using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public Transform Guy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Guy.position - transform.position.normalized;
        rb.linearVelocity = direction * speed;
    }
}
