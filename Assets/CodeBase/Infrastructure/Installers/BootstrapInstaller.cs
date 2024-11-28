using CodeBase.Infrastructure.States;
using CodeBase.Services.AssetManagment;
using CodeBase.Services.CameraProvider;
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
            Container.BindInterfacesTo<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
            Container.BindInterfacesTo<SceneLoader>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
            Container.BindInterfacesTo<AssetProvider>().AsSingle();
            Container.BindInterfacesTo<TimeCounter>().AsSingle();
            Container.BindInterfacesTo<ProgressService>().AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<PlayerProvider>().AsSingle();
            Container.BindInterfacesTo<RaycasterService>().AsSingle();
            Container.BindInterfacesTo<CameraProvider>().AsSingle();
            
        }

        private void RegisterFactories()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
        }
    }
}
