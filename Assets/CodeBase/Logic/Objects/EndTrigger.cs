using System;
using CodeBase.Logic.Player;
using CodeBase.Services.Factory;
using UnityEngine;
using Zenject;

public class EndTrigger : MonoBehaviour
{
    private IPrefabFactory _prefabFactory;

    private bool _alreadyExited;
    
    
    [Inject]
    private void Construct(IPrefabFactory prefabFactory)
    {
        _prefabFactory = prefabFactory;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_alreadyExited)
            return;
        
        if (col.GetComponent<Player>() == null)
            return;

        _alreadyExited = true;
        _prefabFactory.CreateUIFinalPanel();
    }
}
