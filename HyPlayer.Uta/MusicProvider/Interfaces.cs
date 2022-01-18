using HyPlayer.Uta.ProvidableItem;

namespace HyPlayer.Uta.MusicProvider;

/// <summary>
/// 可获取歌词
/// </summary>
public interface ILyricProvidable
{
    /// <summary>
    /// 获取歌词
    /// </summary>
    /// <param name="singleSongBase">单曲</param>
    /// <returns>歌词 LRC</returns>
    public Task<string> GetSongLyric(SingleSongBase singleSongBase);
}

/// <summary>
/// 可获取歌词翻译
/// </summary>
public interface ITranslatedLyricProvidable
{
    /// <summary>
    /// 获取歌词翻译
    /// </summary>
    /// <param name="singleSongBase">单曲</param>
    /// <returns>歌词 LRC</returns>
    public Task<string> GetSongTranslatedLyric(SingleSongBase singleSongBase);
}

/// <summary>
/// 可根据 InProviderId 获取 ProvidableItem
/// </summary>
public interface IProvidableItemConstructable
{
    /// <summary>
    /// 根据 InProviderId 获取 ProvidableItem
    /// </summary>
    /// <param name="inProviderId"></param>
    /// <returns>ProvidableItem</returns>
    public Task<ProvidableItemBase> GetProvidableItemByInProviderId(string inProviderId);
    
    /// <summary>
    /// 根据 InProviderId 列表 获取对应的 ProvidableItem 列表
    /// </summary>
    /// <param name="inProviderIds">InProviderId 列表</param>
    /// <returns>ProvidableItem 列表</returns>
    public Task<List<ProvidableItemBase>> GetProvidableItemsByInProviderIds(List<string> inProviderIds);
}

/// <summary>
/// 可喜欢播放项
/// </summary>
public interface IProvidableItemLikeable
{
    /// <summary>
    /// 喜欢
    /// </summary>
    /// <param name="providableItem">项</param>
    /// <param name="tag">附加参数</param>
    public Task LikeProvidableItem(ProvidableItemBase providableItem,string tag);
    
    /// <summary>
    /// 取消喜欢
    /// </summary>
    /// <param name="providableItem">项</param>
    /// <param name="tag">附加参数</param>
    public Task UnLikeProvidableItem(ProvidableItemBase providableItem,string tag);
}

/// <summary>
/// 可获取播放源的
/// </summary>
public interface IMediaSourceProvidable
{
    /// <summary>
    /// 获取播放源
    /// </summary>
    /// <param name="singleSongBase">单曲</param>
    /// <returns>播放源 (建议为 MediaSource)</returns>
    public Task<object> GetMediaSource(SingleSongBase singleSongBase);
}

/// <summary>
/// 可以搜索的
/// </summary>
public interface ISearchable
{
    /// <summary>
    /// 搜索
    /// </summary>
    /// <param name="keyword">关键词</param>
    /// <param name="typeId">类型 Id</param>
    /// <returns></returns>
    public Task<List<ProvidableItemBase>> SearchProvidableItems(string keyword,string typeId);
}