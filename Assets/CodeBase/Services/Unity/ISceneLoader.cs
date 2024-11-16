using System;

namespace CodeBase.Services.Unity
{
    public interface ISceneLoader
    {
        void LoadAsync(string sceneName, Action onLoaded = null);
        void LoadStraight(string sceneName);
    }
}