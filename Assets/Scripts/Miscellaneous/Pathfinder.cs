using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private WaveConfigSO _waveConfig;

    private List<Transform> waypoints;

    private int _waypointIndex = 0;
    private Transform _nextWaypoint;

    private void Awake()
    {
        _waveConfig = FindObjectOfType<EnemySpawner>().GetWaveConfig();
        waypoints = _waveConfig.GetWaypoints();

        //Move to first waypoint if there is
        if (waypoints.Count > 0)
        {
            transform.position = waypoints[_waypointIndex].position;
        }

        SetNextWaypoint();
    }

    private void Update()
    {
        FollowPath();
    }

    private void SetNextWaypoint()
    {
        if (_waypointIndex < waypoints.Count - 1)
        {
            ++_waypointIndex;
            _nextWaypoint = waypoints[_waypointIndex];
        }
    }

    private void FollowPath()
    {
        /*
         * If enemy reaches end of waypoint, destroy itself
         * **/
        if (_waypointIndex == waypoints.Count - 1)
        {
            Destroy(gameObject);
        }

        float deltaMove = Time.deltaTime * _waveConfig.GetMoveSpeed();

        transform.position = Vector2.MoveTowards(
            transform.position,
            _nextWaypoint.position,
            deltaMove);

        if (transform.position == _nextWaypoint.position)
        {
            SetNextWaypoint();
        }
    }
}
