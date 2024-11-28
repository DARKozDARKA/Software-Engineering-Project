using System;
using CodeBase.Services.InputService;
using UnityEngine;
using Zenject;

namespace CodeBase.Logic.Player
{
    public class PlayerEasyMovement : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 30;

        private IInputService _inputService;
        private Rigidbody2D _rigidbody;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        
        public void Update()
        {
           _rigidbody.AddForce(new Vector2(_inputService.GetHorizontalDirection(), _inputService.GetVerticalDirection() * (Time.deltaTime * _speed)), ForceMode2D.Impulse);
        }
    }
}