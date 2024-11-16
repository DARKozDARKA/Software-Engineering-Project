using CodeBase.Services.Unity;
using CodeBase.StaticData.Enums;
using CodeBase.StaticData.Strings;
using Zenject;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private ISceneLoader _sceneLoader;
        private Difficulties _difficulty;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine, ISceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.LoadAsync(SceneNames.MainMenu);
        }

        public void Exit() { }
    }
}