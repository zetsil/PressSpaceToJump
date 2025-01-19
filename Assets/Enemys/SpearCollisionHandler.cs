using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollisionHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> soundClips; // List of sound clips

    void Start()
    {
        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collides with an object tagged "SpearTip"
        if (collision.gameObject.CompareTag("SpearTip"))
        {
            MakeGameObjectRed();
            Debug.Log("Collided with spear tip!");
            PlayRandomSound();
            AddUpForce(); // Perform another jump or action
            
            // Remove the collider component from the object
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                Destroy(collider);
            }

            Destroy(gameObject, 2f); // Destroy the object after 2 seconds
        }
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
