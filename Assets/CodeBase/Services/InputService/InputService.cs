using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.W))
                OnJumpPressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Mouse0))
                OnFirePressed?.Invoke();
        }

        public float GetHorizontalDirection() => 
            Input.GetAxis("Horizontal");

        public Action OnJumpPressed { get; set; }
        public Action OnFirePressed { get; set; }

    }
}