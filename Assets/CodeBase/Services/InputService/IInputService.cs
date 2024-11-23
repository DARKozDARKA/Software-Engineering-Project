using System;

namespace CodeBase.Services.InputService
{
    public interface IInputService
    {
        float GetHorizontalDirection();
        Action OnJumpPressed { get; set; }
        Action OnFirePressed { get; set; }
    }
}