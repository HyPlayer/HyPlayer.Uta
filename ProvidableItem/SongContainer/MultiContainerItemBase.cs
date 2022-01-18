namespace HyPlayer.Uta.ProvidableItem.SongContainer;

/// <summary>
/// 多子容器式容器
/// </summary>
public abstract class MultiContainerItemBase : ProvidableItemBase
{
    public abstract Task<List<SongContainerBase>> GetChildContainers();
}