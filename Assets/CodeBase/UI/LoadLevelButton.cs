using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData.Strings;
using UnityEngine;
using Zenject;

public class LoadLevelButton : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    [Inject]
    private void Construct(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }

    public void LoadLevel()
    {
        _gameStateMachine.Enter<LoadLevelState, string>(SceneNames.Game);
    }
}
