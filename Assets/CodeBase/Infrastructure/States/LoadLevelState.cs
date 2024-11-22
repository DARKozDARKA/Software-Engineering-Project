using CodeBase.Infrastructure.States.DTO;
using CodeBase.Services.DifficultyService;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using CodeBase.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private GameStateMachine _gameStateMachine;
        private ISceneLoader _sceneLoader;
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticDataService;
        private IPrefabFactory _prefabFactory;
        private IDifficultyService _difficultyService;

        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, IPrefabFactory prefabFactory, IDifficultyService difficultyService)
        {
            _difficultyService = difficultyService;
            _prefabFactory = prefabFactory;
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string name)
        {
            _sceneLoader.LoadAsync(name, OnLoaded);
        }

        public void Exit() { }

        private void OnLoaded()
        {
            _prefabFactory.CreatePlayer(new Vector3(0, 0, 0));
            _prefabFactory.CreateUIRoot();
            GameLoopStateDTO dto = CreateGameLoopStateDTO();
            _stateMachine.Enter<GameLoopState, GameLoopStateDTO>(dto);
        }

        private GameLoopStateDTO CreateGameLoopStateDTO() =>
            new GameLoopStateDTO();
    }
}