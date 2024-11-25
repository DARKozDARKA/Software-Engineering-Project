using System;
using CodeBase.Services.CameraProvider;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.InputService
{
    public class InputService : IInputService, ITickable
    {
        private ICameraProvider _cameraProvider;

        public InputService(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        
        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                OnJumpPressed?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
                OnFirePressed?.Invoke();
        }

        public float GetHorizontalDirection() => 
            Input.GetAxis("Horizontal");
        
        public float GetVerticalDirection() => 
            Input.GetAxis("Vertical");

        public Vector2 GetMousePosition()
        {
            Vector3 mouseScreenPosition = GetScreenMousePosition();

            Camera camera = _cameraProvider.GetCamera();
            
            mouseScreenPosition.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(mouseScreenPosition);
        }

        public Vector2 GetScreenMousePosition() => 
            Input.mousePosition;

        public Action OnJumpPressed { get; set; }
        public Action OnFirePressed { get; set; }

    }
}