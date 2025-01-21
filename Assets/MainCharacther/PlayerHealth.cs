using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance; 

    public int maxHealth = 3; // Number of hearts
    public int currentHealth;

    public Image[] heartImages; 
    public GameObject gameOverPanel;


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

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over Panel not assigned in the Inspector!");
        }
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
            PlayerDeath();
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

    void PlayerDeath()
    {
        Debug.Log("Player Died!");

        // Activează panoul de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Opreste timpul cand moare playerul
        }

    }
}