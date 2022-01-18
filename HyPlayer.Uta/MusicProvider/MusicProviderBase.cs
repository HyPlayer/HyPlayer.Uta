namespace HyPlayer.Uta.MusicProvider;

/// <summary>
/// 音乐提供者
/// </summary>
public abstract class MusicProviderBase
{
    /// <summary>
    /// 音乐提供者友好名称
    /// </summary>
    public string ProviderName;
    
    /// <summary>
    /// 音乐提供者 Id
    /// </summary>
    public string ProviderId;

    /// <summary>
    /// TypeId => 友好名称
    /// </summary>
    public Dictionary<string, string> Types = new();
}