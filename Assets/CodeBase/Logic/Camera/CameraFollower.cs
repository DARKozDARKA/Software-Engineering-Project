using System;
using CodeBase.Logic.Player;
using UnityEngine;
using Zenject;

public class CameraFollower : MonoBehaviour
{
    private IPlayerProvider _playerProvider;
    private Player _player;
    private bool _isPlayerInitialized;

    [SerializeField]
    private Camera _camera;
    
    [SerializeField]
    private Vector3 _offset;
    
    [SerializeField]
    private Vector2 _gridOffset;

    [Inject]
    public void Construct(IPlayerProvider playerProvider)
    {
        _playerProvider = playerProvider;
    }

    private void Awake()
    {
        _playerProvider.OnPlayerInitialized += OnPlayerInitialized;
    }

    private void OnDestroy() => 
        _playerProvider.OnPlayerInitialized -= OnPlayerInitialized;

    private void OnPlayerInitialized(Player player)
    {
        _player = player;
        _isPlayerInitialized = true;
    }

    private void Update()
    {
        if (!_isPlayerInitialized)
            return;

        SnapToPlayer();
    }
    
    
    private void SnapToPlayer()
    {
        float cameraHeight = _camera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * _camera.aspect;

        Vector3 targetPosition = _player.transform.position + _offset;

        float snappedX = Mathf.Floor(targetPosition.x / cameraWidth) * cameraWidth + cameraWidth / 2f;
        float snappedY = Mathf.Floor(targetPosition.y / cameraHeight) * cameraHeight + cameraHeight / 2f;
        
        snappedX += _gridOffset.x * cameraWidth;
        snappedY += _gridOffset.y * cameraHeight;

        transform.position = new Vector3(snappedX, snappedY, targetPosition.z);
    }
}
