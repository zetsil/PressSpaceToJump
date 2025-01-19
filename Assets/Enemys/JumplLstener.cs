using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IObserver;
public class JumpListener : MonoBehaviour , IObserver

{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float jumpBufferTime = 0.3f;
    private float jumpBufferTimer = 0.3f;
    private bool isJumping = false;

    private List<IObserver> listeners;


    // Start is called before the first frame update
    void Start()
    {
        Jump.Instance.AddObserver(this);
        rb = GetComponent<Rigidbody2D>();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            isJumping = false; // Reset jumping state
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

    void OnCollisionStay2D(Collision2D collision)
    {
        // Check if the object exits collision with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }
  

    void Update()
    {
        // Handle Jump Buffer
        if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer -= Time.deltaTime;
        }

                
        // Handle Jumping
        if (isGrounded)
        {
            jumpBufferTimer = jumpBufferTime;
            isJumping = false; 
        }
    }

    public void OnJump()
    {
        // Implement what happens when the jump signal is received
        // For example:
        // - Trigger a jump animation
        // - Change the enemy's behavior (e.g., increase speed, change direction)
        // - Play a sound effect
        if (jumpBufferTimer > 0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferTimer = 0f;
            isGrounded = false;
            isJumping = true;
            Debug.Log("RunnerEnemy jumped!");
        } 
        Debug.Log("RunnerEnemy received Jump signal!"); 
    }

    private void OnDestroy()
    {

        Jump.Instance.RemoveObserver(this);
        OnEvent("EnemyDied", this);
    }

     public void OnEvent(string signal, object o){
        if(signal == "Jump")
            OnJump();
     }


}
