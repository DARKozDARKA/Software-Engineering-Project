using UnityEngine;

namespace CodeBase.Services.CameraProvider
{
    public class CameraProvider : ICameraProvider
    {
        private Camera _camera;

        public void SetCamera(Camera camera) => 
            _camera = camera;

        public Camera GetCamera()
        {
            if (_camera == null) 
                SetCamera(Camera.main);

            return _camera;
        }
    }
}