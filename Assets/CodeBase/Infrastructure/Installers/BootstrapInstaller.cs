using CodeBase.Infrastructure.States;
using CodeBase.Services.AssetManagment;
using CodeBase.Services.DifficultyService;
using CodeBase.Services.DynamicData;
using CodeBase.Services.Factory;
using CodeBase.Services.InputService;
using CodeBase.Services.StaticData;
using CodeBase.Services.Unity;
using CodeBase.Tools;
using UnityEngine;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        [SerializeField]
        private CoroutineRunner _coroutineRunner;

        public override void InstallBindings() => 
            RegisterServices();

        public override void Start()
        {
            base.Start();
            Initialize();
        }

        public void Initialize() =>
            Container.Resolve<GameStateMachine>()
                .With(_ => _.CreateStates())
                .Enter<BootstrapState>();

        private void RegisterServices()
        {
            RegisterUtilities();
            RegisterFactories();
            
            Container.Bind<GameStateMachine>().AsSingle().NonLazy();
        }

        private void RegisterUtilities()
        {
            Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IDifficultyService>().To<DifficultyService>().AsSingle();
            Container.Bind<ITimeCounter>().To<TimeCounter>().AsSingle();
            Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IInputServiceProvider>().To<InputServiceProvider>().AsSingle();
        }

        private void RegisterFactories()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
        }
    }
}
