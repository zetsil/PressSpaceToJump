using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; 

    public int maxHealth = 3; // Number of hearts
    public int currentHealth;

    public Image[] heartImages; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartUI();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            TakeDamage(1); 
            Destroy(other.gameObject);
        }
    }

   void TakeDamage(int damageAmount)
   {
        currentHealth -= damageAmount;

        // Trigger screen shake
        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.2f, 0.3f)); // Adjust duration and magnitude as needed
        }

        if (currentHealth <= 0)
        {
            // Handle player death (e.g., Game Over)
            Debug.Log("Player Died!");
        }

        UpdateHeartUI();
    }
    
    void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentHealth)
            {
                heartImages[i].enabled = true; 
            }
            else
            {
                heartImages[i].enabled = false; 
            }
        }
    }
}