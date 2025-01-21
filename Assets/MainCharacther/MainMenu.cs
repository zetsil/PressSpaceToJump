using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuPanel; // Panoul meniului
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private string volumePrefsKey = "Volume";

    private bool isGamePaused = true; // Variabilă pentru a urmări starea pauzei

    private void Start()
    {
        Time.timeScale = 0f; // Oprim timpul pentru a pune jocul pe pauză
        // Încărcăm volumul salvat anterior (dacă există)
        if (PlayerPrefs.HasKey(volumePrefsKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(volumePrefsKey);
            volumeSlider.value = savedVolume;
            AudioListener.volume = savedVolume;
        }
        else
        {
            volumeSlider.value = 1f; // Valoare implicită
            AudioListener.volume = 1f; // Valoare implicită
        }

        if (mainMenuPanel == null)
        {
            Debug.LogError("Main Menu Panel not assigned in the Inspector!");
        }
    }

    public void PlayGame()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(false);
            Time.timeScale = 1f; // Reluăm timpul (dacă a fost oprit)
            isGamePaused = false;
        }
    }

    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        WaveManager.Instance.ResetWaveManager();
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    public void OnVolumeChanged(float volume)
    {
        volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumePrefsKey, volume);
        PlayerPrefs.Save();
    }

    public void OpenMenu()
    {
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
            Time.timeScale = 0f; // Oprim timpul pentru a pune jocul pe pauză
            isGamePaused = true;
        }
    }

    // Funcție pentru a verifica dacă jocul este pe pauză (poate fi apelată din alte scripturi)
    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    //Exemplu de apelare a meniului cu tasta Escape
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                PlayGame(); // Resume game
            }
            else
            {
                OpenMenu(); // Pause game
            }
        }
    }
}