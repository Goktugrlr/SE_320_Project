using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WaveManager : MonoBehaviour
{
    public float waveDuration = 60.0f;
    private float waveTimer;
    private int waveNumber = 1;
    private bool waveInProgress = false;
    public List<EnemySpawner> enemySpawners;
    private int activeEnemies = 0;
    public TMP_Text waveOverText;
    public Button nextWaveButton;
    // Start is called before the first frame update
    void Start()
    {
        nextWaveButton.onClick.AddListener(OnNextWaveButtonClicked);
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        if(activeEnemies == 0 && !waveInProgress)
        {
            showWaveOverMenu();
        }
        if(waveInProgress)
        {
            waveTimer -= Time.deltaTime;
            if(waveTimer <= 0)
            {
                waveInProgress = false;
                StopSpawningEnemies();
            }

        }
    }

    public void StartWave()
    {
        waveInProgress = true;
        waveTimer = waveDuration;
        foreach(var spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
        nextWaveButton.gameObject.SetActive(false);
        waveOverText.gameObject.SetActive(false);
    }

    public void StartSpawningEnemies()
    {
        foreach(var spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
        activeEnemies++;
    }

    private void StopSpawningEnemies()
    {
        foreach(var spawner in enemySpawners)
        {
            spawner.StopSpawning();
        }
    }

    public void OnNextWaveButtonClicked()
    {
        if(!waveInProgress)
        {
            waveNumber++;
            StartWave();
        }
    }

    public void EnemyDefeated()
    {
        activeEnemies--;
    }

    private void showWaveOverMenu()
    {
        Debug.Log("Basladi");
        waveOverText.text = "Wave " + waveNumber + " Complete!";
        nextWaveButton.gameObject.SetActive(true);
    }

}