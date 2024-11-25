using System;
using UnityEngine;

namespace CodeBase.Services.InputService
{
    public interface IInputService
    {
        float GetHorizontalDirection();
        float GetVerticalDirection();
        Vector2 GetMousePosition();
        Action OnJumpPressed { get; set; }
        Action OnFirePressed { get; set; }
    }
}