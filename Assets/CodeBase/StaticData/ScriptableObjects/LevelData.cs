using UnityEngine;

namespace CodeBase.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/LevelData")]
    public class LevelData : ScriptableObject
    {
        public string SceneName;
        public Vector3 PlayerStartPoint;
    }
}