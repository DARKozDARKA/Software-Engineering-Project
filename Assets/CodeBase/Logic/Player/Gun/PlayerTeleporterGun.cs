using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Logic.Player;
using CodeBase.Services.Factory;
using CodeBase.Services.InputService;
using CodeBase.StaticData.ScriptableObjects;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerTeleporterGun : MonoBehaviour, IPlayerDataRequired
{
    [FormerlySerializedAs("bulletTransform")]
    [SerializeField]
    private Transform _muzzleTransform;

    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private Collider2D _gunCollider;

    private float _reloadTime = 1;
    
    private bool _isReloaded = true;
    private bool _enabled = true;
    private bool _canFire => _isReloaded && _enabled;
    
    private IInputService _inputService;
    private IPrefabFactory _prefabFactory;
    private Vector3 _currentDirection;

    [Inject]
    private void Construct(IInputService inputService, IPrefabFactory prefabFactory)
    {
        _prefabFactory = prefabFactory;
        _inputService = inputService;
    }

    private void Start()
    {
        _inputService.OnFirePressed += OnFirePressed;
    }
    
    private void OnDestroy()
    {
        _inputService.OnFirePressed -= OnFirePressed;
    }
    
    public void LoadData(PlayerData playerData)
    {
        _reloadTime = playerData.GunReloadTime;
    }

    public void EnableGun() => 
        _enabled = true;

    public void DisableGun() => 
        _enabled = false;

    private void Update()
    {
        Vector3 mousePos = _inputService.GetMousePosition();
        _currentDirection = mousePos - transform.position;
        float rotationZ = Mathf.Atan2(_currentDirection.y, _currentDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private void OnFirePressed()
    {
        if (!_canFire)
            return;

        List<Collider2D> results = new List<Collider2D>();
        if (_gunCollider.Overlap(results) != 0)
            return;

        _prefabFactory.CreateTeleportProjectile(_muzzleTransform.transform.position, _currentDirection)
            .With(_ => _.OnSurfaceHit += OnSurfaceHit);

        StartCoroutine(Reload());
    }

    private void OnSurfaceHit(TeleporterProjectile projectile)
    {
        _playerMovement.TeleportTo(projectile.GetPosition());
        projectile.OnSurfaceHit -= OnSurfaceHit;
    }

    private IEnumerator Reload()
    {
        _isReloaded = false;
        yield return new WaitForSeconds(_reloadTime);
        _isReloaded = true;
    }



}
