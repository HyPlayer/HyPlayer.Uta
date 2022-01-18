namespace HyPlayer.Uta.ProvidableItem.SongContainer;

/// <summary>
/// 歌曲容器
/// </summary>
public abstract class SongContainerBase : ProvidableItemBase
{
    /// <summary>
    /// 创建者
    /// </summary>
    public MultiContainerItemBase Creator;

    /// <summary>
    /// 简介
    /// </summary>
    public string Description;

    /// <summary>
    /// 获取容器封面
    /// </summary>
    /// <returns>容器封面 建议返回 BitmapImage</returns>
    public abstract Task<object> GetCoverImage();
}