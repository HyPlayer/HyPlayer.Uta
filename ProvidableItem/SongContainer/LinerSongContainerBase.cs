namespace HyPlayer.Uta.ProvidableItem.SongContainer;

/// <summary>
/// 线性播放容器
/// 也就是可以读取到底的
/// </summary>
public abstract class LinerSongContainerBase : SongContainerBase
{
    /// <summary>
    /// 获取容器中的所有歌曲
    /// </summary>
    /// <returns></returns>
    public abstract Task<List<SingleSongBase>> GetContainerItems();
}