using System;
using CodeBase.Logic.Player;
using UnityEngine;

public class PlayerProvider : IPlayerProvider
{
    private Player _player;

    public Action<Player> OnPlayerInitialized { get; set; }

    public void SetPlayer(Player player)
    {
        _player = player;
        OnPlayerInitialized?.Invoke(_player);
    }

    public Player GetPlayer()
    {
        return _player;
    }
}
