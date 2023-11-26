using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Spawn Settings")]
    [Space(5)]
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private bool isInfiniteWave;

    private WaveConfigSO _currentWaveConfig;
    private int _waveConfigIndex = -1;

    private void Start()
    {
        SetNextWaveConfig();
        StartCoroutine(SpawnEnemy());
    }

    private void SetNextWaveConfig()
    {
        if (_waveConfigIndex < waveConfigs.Count - 1)
        {
            ++_waveConfigIndex;
            _currentWaveConfig = waveConfigs[_waveConfigIndex];
        }
    }

    public WaveConfigSO GetWaveConfig() => _currentWaveConfig;

    private IEnumerator SpawnEnemy()
    {
        do
        {
            foreach (WaveConfigSO waveConfig in waveConfigs)
            {
                for (int i = 0; i < waveConfig.GetEnemyCount(); i++)
                {
                    float spawnTime = waveConfig.GetRandomSpawnTime();
                    yield return new WaitForSeconds(spawnTime);

                    Instantiate(
                        waveConfig.GetEnemyPrefab(i),
                        waveConfig.GetStartingWaypoint().position,
                        Quaternion.Euler(0f, 0f, -180f),
                        transform);
                }

                SetNextWaveConfig();
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isInfiniteWave);
        
    }
}
