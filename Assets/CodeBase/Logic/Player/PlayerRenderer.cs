using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class PlayerRenderer : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement _playerMovement;

        private PlayerDirections _currentDirection;

        private void Update()
        {
            if (_currentDirection == _playerMovement.PlayerDirection)
                return;

            _currentDirection = _playerMovement.PlayerDirection;

            if (_currentDirection == PlayerDirections.Idle)
                return;
            
            transform.localScale = _currentDirection == PlayerDirections.Right ?
                    new Vector3(1, transform.localScale.y, transform.localScale.z)
                    : new Vector3(-1, transform.localScale.y, transform.localScale.z);


        }
    }
}