using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    public float offsetDistance = 2f; // Distance from the player
    private Transform player;
    private Rigidbody2D rb;
    private bool movingRight = false; // Start by checking if we need to move right
    private bool isGrounded = false;

    public float uprightThreshold = 30f; // Degrees allowed from upright before correcting
    public float correctionTorque = 10f; // Torque applied to correct rotation

    private Vector3 scale;

    void Start()
    {
        scale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the 'Player' tag.");
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found!");
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }    

    void OnCollisionExit2D(Collision2D collision)
    {
        // Check if the object exits collision with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
  

    void Update()
    {


        // Check if the object is overturned
        CheckAndCorrectOrientation();
    }

    void FixedUpdate()
    {
        if (player == null || rb == null) return;

        if (isGrounded) // Only change direction if grounded
        {
            float targetX;

            if (!movingRight)
            {
                targetX = player.position.x - offsetDistance;
            }
            else
            {
                targetX = player.position.x + offsetDistance;
            }

            float horizontalDirection = targetX - transform.position.x;

            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.x = 0;

            if (Mathf.Abs(horizontalDirection) > 0.1f)
            {
                if (horizontalDirection > 0)
                {
                    currentVelocity.x = speed;
                    scale.x = Mathf.Sign(1) * Mathf.Abs(scale.x); // Flip based on direction
                }
                else
                {
                    currentVelocity.x = -speed;
                    scale.x = Mathf.Sign(-1) * Mathf.Abs(scale.x); // Flip based on direction

                }
            }
            else
            {
                currentVelocity.x = 0;
                movingRight = !movingRight;
            }
            rb.linearVelocity = currentVelocity;
        }
        else
        {
            //If it is not grounded maintain the velocity
            Vector2 currentVelocity = rb.linearVelocity;
            currentVelocity.x = rb.linearVelocity.x;
            rb.linearVelocity = currentVelocity;
            transform.localScale = scale;
        }

        //        if (horizontalInput != 0 && !isDashing)
        // {
        //     transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        //     // Handle Object Flip
        //     Vector3 scale = transform.localScale;
        //     scale.x = Mathf.Sign(horizontalInput) * Mathf.Abs(scale.x); // Flip based on direction
        //     transform.localScale = scale;
        // }
    }

    void CheckAndCorrectOrientation()
    {
        // Get the current rotation angle
        float angle = transform.eulerAngles.z;

        // Normalize the angle to the range [-180, 180]
        if (angle > 180) angle -= 360;

        // If the angle is outside the upright threshold, apply correction
        if (Mathf.Abs(angle) > uprightThreshold)
        {
            // Apply torque to rotate the object upright
            float correctionDirection = angle > 0 ? -1 : 1; // Determine the correction direction
            rb.AddTorque(correctionDirection * correctionTorque);
        }
    }
}
