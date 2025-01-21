using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f; // The speed of the ball
    private Vector2 direction; // The current direction of the ball

    void Start()
    {
        // Set an initial random direction with more variance
        direction = Random.insideUnitCircle.normalized; 
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reflect the ball's direction with slight randomness
        Vector2 normal = collision.contacts[0].normal;
        direction = Vector2.Reflect(direction, normal).normalized;

        // Add some random jitter to the direction
        direction += Random.insideUnitCircle * 0.3f; // Adjust 0.1f for desired randomness
        direction = direction.normalized; 

        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }
}