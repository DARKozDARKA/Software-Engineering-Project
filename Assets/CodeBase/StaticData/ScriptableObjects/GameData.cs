using UnityEngine;

namespace CodeBase.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/GameData")]
    public class GameData : ScriptableObject
    {
        public float PlayerUpgradeTime;
    }
}