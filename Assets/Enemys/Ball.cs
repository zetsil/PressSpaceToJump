using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // The speed of the ball
    private Vector2 direction; // The current direction of the ball

    void Start()
    {
        // Set an initial random direction
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the ball's direction based on the collision normal
        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }
}