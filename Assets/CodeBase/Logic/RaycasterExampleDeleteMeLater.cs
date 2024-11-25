using System;
using CodeBase.Logic.Player;
using CodeBase.Services.InputService;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

public class RaycasterExampleDeleteMeLater : MonoBehaviour
{
    [SerializeField]
    private GameObject _object;

    [SerializeField]
    private LineRenderer _line;
    
    private IRaycasterService _raycasterService;
    private IInputService _inputService;

    [Inject]
    private void Construct(IRaycasterService raycasterService, IInputService inputService)
    {
        _inputService = inputService;
        _raycasterService = raycasterService;
    }

    private void Awake()
    {
        _inputService.OnFirePressed += OnFirePressed;
        _line.positionCount = 2;
    }

    private void OnDestroy()
    {
        _inputService.OnFirePressed -= OnFirePressed;
    }

    private void Update()
    {
        OnFirePressed();
    }

    private void OnFirePressed()
    {
        RaycastHit2D hit = _raycasterService.Raycast(_inputService.GetMousePosition(),  _inputService.GetMousePosition() - _object.transform.position.ToVector2());
        _line.SetPosition(0, _object.transform.position);
        _line.SetPosition(1, hit.point);
    }
    
}
