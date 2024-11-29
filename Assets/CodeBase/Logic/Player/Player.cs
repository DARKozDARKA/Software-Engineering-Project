using System;
using UnityEngine;

namespace CodeBase.Logic.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement _playerMovement;
        
        [SerializeField]
        private PlayerTeleporterGun _playerGun;

        private void Start()
        {
            _playerMovement.OnStateChanged += OnMovementStateChanged;
        }

        private void OnDestroy()
        {
            _playerMovement.OnStateChanged -= OnMovementStateChanged;

        }

        private void OnMovementStateChanged(PlayerMovementState state)
        {
            switch (state)
            {
                case PlayerMovementState.Default:
                    _playerGun.EnableGun();
                    break;
                case PlayerMovementState.Slope:
                    _playerGun.DisableGun();
                    break;
            }
        }
    }
}