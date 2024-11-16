using System;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Enums;
using CodeBase.StaticData.ScriptableObjects;
using Zenject;

namespace CodeBase.Services.DifficultyService
{
    public class DifficultyService : IDifficultyService
    {
        public Action<DifficultyData> OnDifficultyChanged { get; set; }
        
        private Difficulties _difficulty = Difficulties.Normal;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void SetDifficulty(Difficulties difficulty)
        {
            _difficulty = difficulty;
            OnDifficultyChanged?.Invoke(_staticDataService.GetDifficultyData(_difficulty));
        }

        public Difficulties GetDifficulty() =>
            _difficulty;
        
        public DifficultyData GetDifficultyData() =>
            _staticDataService.GetDifficultyData(_difficulty);
    }
}