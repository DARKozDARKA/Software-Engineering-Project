using System;
using CodeBase.Logic.Player;

public interface IPlayerProvider
{
    Action<Player> OnPlayerInitialized { get; set; }
    void SetPlayer(Player player);
    Player GetPlayer();
}