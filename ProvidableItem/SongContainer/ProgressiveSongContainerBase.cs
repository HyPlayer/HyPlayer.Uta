namespace HyPlayer.Uta.ProvidableItem.SongContainer;

/// <summary>
/// 渐进式播放容器
/// </summary>
public abstract class ProgressiveSongContainer : SongContainerBase
{
    /// <summary>
    /// 获取接下来的歌曲列表
    /// </summary>
    /// <returns>歌曲列表</returns>
    public abstract Task<List<SingleSongBase>> GetNextItems();
}