using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;        // Regular movement speed
    [SerializeField] private float dashSpeed = 15f;      // Speed during a dash
    [SerializeField] private float dashDuration = 0.2f;  // Duration of the dash
    [SerializeField] private float dashCooldown = 1f;    // Cooldown time between dashes

    private bool isDashing = false;      // Whether the character is currently dashing
    private float dashCooldownTimer = 0f; // Timer to track cooldown

    void Update()
    {
        // Handle Dash Cooldown
        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        // Handle Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (horizontalInput != 0 && !isDashing)
        {
            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

            // Handle Object Flip
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(horizontalInput) * Mathf.Abs(scale.x); // Flip based on direction
            transform.localScale = scale;
        }

        // Handle Dash Input
        if (Input.GetKeyDown(KeyCode.LeftShift) && horizontalInput != 0 && dashCooldownTimer <= 0f)
        {
            StartCoroutine(Dash(horizontalInput));
        }
    }

    private System.Collections.IEnumerator Dash(float direction)
    {
        isDashing = true; // Set dashing flag
        dashCooldownTimer = dashCooldown; // Reset cooldown timer

        float dashEndTime = Time.time + dashDuration;

        while (Time.time < dashEndTime)
        {
            // Move quickly in the dash direction
            transform.Translate(Vector3.right * direction * dashSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        isDashing = false; // Reset dashing flag
    }
}
