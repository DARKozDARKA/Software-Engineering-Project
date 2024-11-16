using CodeBase.Services.AssetManagment;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Strings;
using CodeBase.Tools;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.Factory
{
    public class PrefabFactory : IPrefabFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly DiContainer _container;
        private GameObject _uiRoot;

        public PrefabFactory(DiContainer container, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _container = container;
        }

        public GameObject CreatePlayer(Vector3 at) =>
            _assetProvider.Instantiate(PrefabsPath.Player, at)
                .With(Inject);

        public GameObject CreateUIRoot() =>
            _assetProvider.Instantiate(PrefabsPath.UI)
                .With(Inject)
                .With(_ => _uiRoot = _);
        

        private void Inject(GameObject gameObject) =>
            _container.InjectGameObject(gameObject);
    }
}