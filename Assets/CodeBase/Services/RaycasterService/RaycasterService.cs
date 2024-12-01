using CodeBase.StaticData.Strings;
using UnityEngine;

public class RaycasterService : IRaycasterService
{
    public RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, direction.normalized, Mathf.Infinity, Physics2D.DefaultRaycastLayers);
        return hit;
    }

    public Vector3 AdjustPosition(Vector3 targetPosition, Vector2 colliderSize, float adjustmentValue)
    {
        Vector2 boxSize = new Vector2(colliderSize.x, colliderSize.y);  // Adjust the box size as needed for your player
    
        Collider2D hit = Physics2D.OverlapBox(targetPosition, boxSize, 0f, LayerMask.GetMask(LayersName.Terrain));
    
        if (hit != null)
            return AdjustPositionByDirections(targetPosition, adjustmentValue);

        return targetPosition;
    }

    private Vector3 AdjustPositionByDirections(Vector3 targetPosition, float adjustmentValue)
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector3 adjustedPosition = targetPosition;

        foreach (Vector2 direction in directions) 
            AdjustPositionBySide(direction);

        return adjustedPosition;

        void AdjustPositionBySide(Vector2 direction)
        {
            RaycastHit2D raycast =
                Physics2D.Raycast(targetPosition,
                    direction,
                    adjustmentValue,
                    LayerMask.GetMask(LayersName.Terrain));

            if (!raycast.collider)
                adjustedPosition += (Vector3)direction * adjustmentValue;
        }
    }
}
