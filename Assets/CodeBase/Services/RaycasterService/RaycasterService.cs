using UnityEngine;

public class RaycasterService : IRaycasterService
{
    public RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, Mathf.Infinity, Physics2D.DefaultRaycastLayers);
        return hit;
    }
}
