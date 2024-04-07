using System.Collections;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject dayBanditPrefab;
    [SerializeField] GameObject nightBanditPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeBetweenWaves = 2f;
    [SerializeField] int initialBanditsCount = 3;
    [SerializeField] float increaseSpawnRate = 0.1f;
    [SerializeField] float increaseBanditsCount = 1;

    [SerializeField] int currentWave = 0;
    [SerializeField] int currentBanditsCount = 2;
    
    private PlayerController playerController;

    void Start()
    {
        // Find the GameManager in the scene
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
            Debug.LogError("GameManager not found in the scene.");
        // Find the PlayerController in the scene
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        // Start the wave system
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            if (playerController.GetLives() > 0)
                SpawnBandits();
        }
    }

    void SpawnBandits()
    {
        GameObject[] bandits = GameObject.FindGameObjectsWithTag("Enemy");
        //if there are bandits in the scene, return
        if (bandits.Length > 0)
            return;
        currentWave++;
        for (int i = 0; i < currentBanditsCount; i++)
        {
            GameObject banditPrefab = null;
            // Determine which type of bandit to spawn based on the GameManager's typeOfSpawn
            if (gameManager.typeOfSpawn == "Day")
                banditPrefab = dayBanditPrefab;
            else
                banditPrefab = nightBanditPrefab;
            // Spawn the selected bandit
            if (banditPrefab != null)
                Instantiate(banditPrefab, spawnPoint.position, Quaternion.identity);
        }
        // Increase bandit count and spawn rate for each new wave
        currentBanditsCount += Mathf.RoundToInt(increaseBanditsCount * currentWave);
        currentBanditsCount = Mathf.Clamp(currentBanditsCount, 1, 10);
        timeBetweenWaves -= increaseSpawnRate * currentWave;
        // Ensure minimum bandit count and spawn rate
        currentBanditsCount = Mathf.Max(currentBanditsCount, initialBanditsCount);
        timeBetweenWaves = Mathf.Max(timeBetweenWaves, 5f);
    }
}