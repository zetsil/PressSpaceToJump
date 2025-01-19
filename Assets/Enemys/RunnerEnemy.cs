using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IObserver;
public class RunnerEnemy : MonoBehaviour , IObserver

{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float jumpBufferTime = 0.3f;
    private float jumpBufferTimer = 0.3f;
    private bool isJumping = false;
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> soundClips; // List of sound clips


    // Start is called before the first frame update
    void Start()
    {
        Jump.Instance.AddObserver(this);
        rb = GetComponent<Rigidbody2D>();

        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with the ground
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            isJumping = false; // Reset jumping state
        }

        // Check if the object collides with an object tagged "SpearTip"
        if (collision.gameObject.CompareTag("SpearTip"))
        {
            MakeGameObjectRed();
            Debug.Log("colide !");
            PlayRandomSound();
            AddUpForce(); // Perform another jump
            Destroy(gameObject,2f); // Destroy the spear tip object
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
        // Handle Jump Buffer
        if (jumpBufferTimer > 0f)
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        
        // Draw the raycast for visualization
        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red); 
        
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

    private void AddUpForce()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>(); 
        if (rb == null) return;

        float jumpForce = 5f; 
        float jumpForceVariation = 20f; 
        float spinForce = 10f;
        float spinForceVariation = 3f;

        float randomJumpForce = jumpForce + Random.Range(15, jumpForceVariation);
        float randomSpinForce = spinForce + Random.Range(-spinForceVariation, spinForceVariation);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, randomJumpForce); 
        rb.angularVelocity = randomSpinForce; 
    }       

    private void OnDestroy()
    {
        Jump.Instance.RemoveObserver(this); 
    }

     public void OnEvent(string signal, object o){
        if(signal == "Jump")
            OnJump();
     }


    void PlayRandomSound()
    {
        if (soundClips == null || soundClips.Count == 0) return;

        // Randomly select a clip from the list
        AudioClip selectedClip = soundClips[Random.Range(0, soundClips.Count)];

        // Assign the selected clip to the AudioSource
        audioSource.clip = selectedClip;

        // Set random pitch
        audioSource.pitch = Random.Range(0.8f, 1.2f); // Random pitch between 0.8 and 1.2

        // Set random volume
        audioSource.volume = Random.Range(0.7f, 1.0f); // Random volume between 0.7 and 1.0

        // Set random stereo pan
        audioSource.panStereo = Random.Range(-1f, 1f); // Random stereo pan between left (-1) and right (1)

        // Set random spatial blend
        audioSource.spatialBlend = Random.Range(0.5f, 1.0f); // Blend between 2D (0) and 3D (1)

        // Set random playback start position (only if the clip length allows)
        audioSource.time = Random.Range(0, selectedClip.length * 0.5f); // Start playback at a random point in the first half

        // Play the sound
        audioSource.Play();
    }

     public void MakeGameObjectRed()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            Debug.LogWarning("No Renderer component found on this GameObject.");
        }
    }

}
