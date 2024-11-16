using UnityEngine;

namespace CodeBase.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public float VerticalSpeed;
        public float HorizontalSpeed;
        public float UpgradeModifier;

        public void Copy(PlayerData playerData)
        {
            VerticalSpeed = playerData.VerticalSpeed;
            HorizontalSpeed = playerData.HorizontalSpeed;
            UpgradeModifier = playerData.UpgradeModifier;
        }
    }
}