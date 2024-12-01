using UnityEngine;

public interface IRaycasterService
{
    RaycastHit2D Raycast(Vector2 origin, Vector2 direction);
    Vector3 AdjustPosition(Vector3 targetPosition, Vector2 colliderSize, float adjustmentValue);
}