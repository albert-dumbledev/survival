using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    [SerializeField]
    private List<SpawnPoint> spawnPoints;
    private float secondsBetweenWaves;
    private int waveCount;
    private float timeSinceLastWave;

    // Start is called before the first frame update
    void Start() {
        waveCount = 0;
        secondsBetweenWaves = 7f;
        timeSinceLastWave = 5f;
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastWave += Time.deltaTime;
        if (timeSinceLastWave >= secondsBetweenWaves) {
            SpawnWave();
            timeSinceLastWave = 0f;
        }
    }

    private void SpawnWave() {
        int spawnTypes = 1;
        if (waveCount >= 5 && waveCount < 10) {
            spawnTypes = 2;
        } else if (waveCount >= 10) {
            spawnTypes = 3;
        }

        waveCount++;
        for (int spawns = 0; spawns < Mathf.Max(CalculateSpawnCount(), 1); spawns++) {
            int randomEnemy = Random.Range(0, Mathf.Min(spawnPoints.Count + 1, spawnTypes));
            Instantiate(spawnPoints[randomEnemy], new Vector2(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2)), Quaternion.identity);
        }
    }

    private float CalculateSpawnCount() {
        int randomSpawnVariation = Random.Range(-waveCount / 2, waveCount / 2);
        return (waveCount % 5) * .7f + waveCount + randomSpawnVariation;
    }
}
