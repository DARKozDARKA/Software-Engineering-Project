using UnityEngine;

namespace CodeBase.Services.Factory
{
    public interface IPrefabFactory
    {
        GameObject CreatePlayer(Vector3 at);
        GameObject CreateUIRoot();
    }
}