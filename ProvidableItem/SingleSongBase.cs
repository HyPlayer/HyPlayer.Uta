using HyPlayer.Uta.ProvidableItem.SongContainer;

namespace HyPlayer.Uta.ProvidableItem;

/// <summary>
/// 单曲
/// </summary>
public abstract class SingleSongBase : ProvidableItemBase
{
    /// <summary>
    /// 专辑
    /// </summary>
    public AlbumBase Album;
    
    /// <summary>
    /// 艺术家
    /// </summary>
    public List<ArtistBase> Artists;
    
    /// <summary>
    /// 艺术家显示文本
    /// </summary>
    public string ArtistString => string.Join(" / ", Artists.Select(x => x.Name));
    
    /// <summary>
    /// 歌曲简介
    /// </summary>
    public string Description;
    
    /// <summary>
    /// 歌名翻译
    /// </summary>
    public string TranslatedName;
    
    /// <summary>
    /// 歌曲时长
    /// </summary>
    public TimeSpan Duration;
    
    /// <summary>
    /// 歌曲是否可播放
    /// </summary>
    public bool Available;
    
    /// <summary>
    /// 歌曲的 Tag 信息
    /// </summary>
    public string[] Tags;

}