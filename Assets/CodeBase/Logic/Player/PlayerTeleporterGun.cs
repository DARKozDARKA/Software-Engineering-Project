using System;
using System.Collections;
using CodeBase.Logic.Player;
using CodeBase.Services.Factory;
using CodeBase.Services.InputService;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerTeleporterGun : MonoBehaviour
{
    [FormerlySerializedAs("bulletTransform")]
    [SerializeField]
    private Transform _muzzleTransform;

    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private float _reloadTime = 1;
    
    private bool _canFire = true;
    
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
        _canFire = false;
        yield return new WaitForSeconds(_reloadTime);
        _canFire = true;
    }


}
