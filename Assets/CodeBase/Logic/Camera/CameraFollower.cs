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
    private Vector3 _offset;

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

        transform.position = _player.transform.position + _offset;
    }
}
