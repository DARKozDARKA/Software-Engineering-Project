using System;
using CodeBase.StaticData.Enums;
using CodeBase.StaticData.ScriptableObjects;

namespace CodeBase.Services.DifficultyService
{
    public interface IDifficultyService
    {
        void SetDifficulty(Difficulties difficulty);
        Difficulties GetDifficulty();
        DifficultyData GetDifficultyData();
        Action<DifficultyData> OnDifficultyChanged { get; set; }
    }
}