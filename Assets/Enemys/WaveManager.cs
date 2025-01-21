using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class WaveManager : MonoBehaviour, IObserver
{
    public static WaveManager Instance;

    public List<GameObject> levels; 
    private int currentLevelIndex = 0; 

    private int currentEnemys = 0;

    public TextMeshProUGUI enemyCountText; // Reference to the TextMeshProUGUI component

    private bool shouldProcessEnemyDeath = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartNextWave();
    }

    public void StartNextWave()
    {
        if (currentLevelIndex < levels.Count)
        {
            GameObject level = Instantiate(levels[currentLevelIndex],transform.position,Quaternion.identity);
            currentEnemys = CountChildren(level.transform);
             // Handle end of game or victory condition
            UpdateEnemyCountText();
            Debug.Log("Current enemys :" + currentEnemys);

            currentLevelIndex++;
        }
        else
        {
            // Handle end of game or victory condition
            Debug.Log("All levels completed!"); 
        }
    }

    public void OnEvent(string notificationType, object a) 
    {
        if(notificationType == "EnemyDied" && shouldProcessEnemyDeath)
        {
            currentEnemys--;
            UpdateEnemyCountText(); 
            Debug.Log("Enemy Died! Remaining: " + currentEnemys);
            if (currentEnemys <= 0) 
            {
                StartNextWave(); 
            }
        }
    }

    public void ResetWaveManager() // Call this when restarting the level
    {
        shouldProcessEnemyDeath = false;
    }

    public int CountChildren(Transform LVLtransform) // count enemys in this lvl
    {
        return LVLtransform.childCount;
    }

    private void UpdateEnemyCountText()
    {
        if (enemyCountText != null) 
        {
            enemyCountText.text = "Enemies Remaining: " + currentEnemys; 
        }
    }

}
