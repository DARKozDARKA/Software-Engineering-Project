using CodeBase.Logic.Player;
using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IPrefabFactory
    {
        Player CreatePlayer(Vector3 at);
        GameObject CreateUIRoot();
        TeleporterProjectile CreateTeleportProjectile(Vector3 position, Vector2 direction);
        GameObject CreateUIFinalPanel();
    }
}