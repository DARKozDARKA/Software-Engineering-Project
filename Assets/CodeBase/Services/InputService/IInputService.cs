using System;
using UnityEngine;

namespace CodeBase.Services.InputService
{
    public interface IInputService
    {
        float GetHorizontalDirection();
        float GetVerticalDirection();
        Vector2 GetMousePosition();
        Vector2 GetScreenMousePosition();

        Action OnJumpPressed { get; set; }
        Action OnFirePressed { get; set; }
    }
}