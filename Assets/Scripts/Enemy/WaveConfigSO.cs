using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Config", menuName = "Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Transform pathParent;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenEnemySpawns;
    [SerializeField] private float spawnTimeVariance;
    [SerializeField] private float minSpawnTime;

    public Transform GetStartingWaypoint() => pathParent.GetChild(0);

    public GameObject GetEnemyPrefab(int index) => enemies[index];

    public int GetEnemyCount() => enemies.Count;

    public float GetMoveSpeed() => moveSpeed;

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new();

        foreach (Transform item in pathParent)
        {
            waypoints.Add(item);
        }

        return waypoints;
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenEnemySpawns - spawnTimeVariance,
            timeBetweenEnemySpawns + spawnTimeVariance);

        return Mathf.Clamp(spawnTime, minSpawnTime, float.MaxValue);
    }
}
