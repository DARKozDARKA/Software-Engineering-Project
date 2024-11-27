﻿using CodeBase.Logic.Player;
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

        public Player CreatePlayer(Vector3 at) =>
            _assetProvider.Instantiate(PrefabsPath.Player, at)
                .With(Inject)
                .GetComponent<Player>();

        public GameObject CreateUIRoot() =>
            _assetProvider.Instantiate(PrefabsPath.UI)
                .With(Inject)
                .With(_ => _uiRoot = _);

        public TeleporterProjectile CreateTeleportProjectile(Vector3 position, Vector2 direction) =>
            _assetProvider.Instantiate(PrefabsPath.TeleportProjectile, position)
                .With(Inject)
                .GetComponent<TeleporterProjectile>()
                .With(_ => _.SetDirection(direction));

        private void Inject(GameObject gameObject) =>
            _container.InjectGameObject(gameObject);
    }
}