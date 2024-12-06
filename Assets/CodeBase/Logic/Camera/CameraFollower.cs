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

    [SerializeField]
    private float _targetAspect;

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


    private void Start() => 
        AdjustCamera();

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

    private void AdjustCamera()
    {
        float targetAspect = _targetAspect;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Rect rect = _camera.rect;

        if (scaleHeight < 1f)
        {
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            float scaleWidth = 1f / scaleHeight;

            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;
        }

        _camera.rect = rect;
    }
}
