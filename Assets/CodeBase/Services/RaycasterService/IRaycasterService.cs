using UnityEngine;

public interface IRaycasterService
{
    RaycastHit2D Raycast(Vector2 origin, Vector2 direction);
}