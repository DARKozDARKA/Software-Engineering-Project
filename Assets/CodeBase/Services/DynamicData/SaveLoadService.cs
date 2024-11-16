using CodeBase.Tools;
using UnityEngine;

namespace CodeBase.Services.DynamicData
{
  public class SaveLoadService : ISaveLoadService
  {
    private IProgressService _progressService;
    private const string ProgressKey = "Progress";

    public SaveLoadService(IProgressService progressService)
    {
      _progressService = progressService;
    }

    public void SaveProgress()
    {
      PlayerPrefs.SetString(ProgressKey, _progressService.PlayerProgress.ToJson());
    }

    public PlayerProgress LoadProgress()
    {
      return PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
    }
  }
}