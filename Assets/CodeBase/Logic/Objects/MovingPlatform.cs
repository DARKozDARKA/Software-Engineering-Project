using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _waypoints; 

    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private bool _useLerp = false; 

    private int _currentWaypointIndex = 0;

    private Vector3 _startPosition; 
    private float _lerpProgress = 0f; 

    private void Start()
    {
        if (_waypoints != null && _waypoints.Count > 0) 
            transform.position = _waypoints[0].transform.position;
    }

    private void Update()
    {
        if (_waypoints == null || _waypoints.Count == 0)
            return;

        if (_useLerp)
            MoveUsingLerp();
        else
            MoveUsingMoveTowards();
    }

    private void MoveUsingMoveTowards()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex].transform;

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, _speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
    }

    private void MoveUsingLerp()
    {
        Transform targetWaypoint = _waypoints[_currentWaypointIndex].transform;

        if (_lerpProgress == 0f) 
            _startPosition = transform.position;

        Vector3 position = targetWaypoint.position;
        _lerpProgress += _speed * Time.deltaTime / Vector3.Distance(_startPosition, position);
        transform.position = Vector3.Lerp(_startPosition, position, _lerpProgress);

        if (!(_lerpProgress >= 1f))
            return;
        
        _lerpProgress = 0f;
        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
    }
}