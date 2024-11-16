using CodeBase.Services.DynamicData;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using CodeBase.StaticData.Strings;
using CodeBase.Tools;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private GameStateMachine _stateMachine;
        private ISceneLoader _sceneLoader;
        private IStaticDataService _staticDataService;
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;

        [Inject]
        public void Construct(GameStateMachine stateMachine, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, ISaveLoadService saveLoadService, IProgressService progressService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            WarmUp();
            _sceneLoader.LoadAsync(SceneNames.Bootstrap, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<MainMenuState>();
        }

        private void WarmUp()
        {
            _staticDataService.Load();
            LoadProgressOrInitNew();
        }
        
        private void LoadProgressOrInitNew() =>
            _progressService.PlayerProgress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();

        private PlayerProgress NewProgress() => new();
    }
}