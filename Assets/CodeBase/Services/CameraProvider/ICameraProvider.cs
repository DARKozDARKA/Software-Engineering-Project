using UnityEngine;

namespace CodeBase.Services.CameraProvider
{
    public interface ICameraProvider
    {
        void SetCamera(Camera camera);
        Camera GetCamera();
    }
}