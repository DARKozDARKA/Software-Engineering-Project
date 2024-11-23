using System.Collections;
using CodeBase.Infrastructure.States.DTO;
using CodeBase.Services.DynamicData;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IPayloadedState<GameLoopStateDTO>
    {
        private GameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;

        private float _upgradeTime;
        private ICoroutineRunner _coroutineRunner;
        private IEnumerator _upgradeCoroutine;
        private ITimeCounter _timeCounter;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private GameLoopStateDTO _gameLoopStateDTO;
        private IPrefabFactory _prefabFactory;

        [Inject]
        public void Construct(IPrefabFactory prefabFactory)
        {
            _prefabFactory = prefabFactory;
        }

        public void Enter(GameLoopStateDTO payload)
        {
            //
        }


        public void Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}