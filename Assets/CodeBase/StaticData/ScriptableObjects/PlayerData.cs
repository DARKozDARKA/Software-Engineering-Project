using UnityEngine;

namespace CodeBase.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Movement Settings")]
        public int JumpPower = 800;
        public float PlayerJumpReload = 0.2f;
        public float GravityScale = 1;
        
        [Header("Player Gun Settings")]
        public float GunReloadTime = 0.3f;
        public float GunProjectileSpeed = 5f;
        public float CollisionAdjustmentAmount = 0.5f;
        public float GunProjectileLifetime = 1;
    }
}