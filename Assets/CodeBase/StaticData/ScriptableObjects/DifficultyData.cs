using CodeBase.StaticData.Enums;
using UnityEngine;

namespace CodeBase.StaticData.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Difficulty", menuName = "StaticData/Difficulty")]
    public class DifficultyData : ScriptableObject
    {
        public Difficulties DifficultyType;
        public string DifficultyName;

        public float PlayerSpeedModifier;
    }
}