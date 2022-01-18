namespace HyPlayer.Uta.ProvidableItem;

/// <summary>
/// 可提供的东西
/// 一切的基础
/// </summary>
public abstract class ProvidableItemBase
{
    /// <summary>
    /// 提供者 Id
    /// </summary>
    public string ProviderId;

    /// <summary>
    /// 类型 Id
    /// </summary>
    public string TypeId;

    /// <summary>
    /// 实际 Id
    /// </summary>
    public string ActualId;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 传入 Provider 的 Id
    /// </summary>
    public string InProviderId => TypeId + ActualId;

    /// <summary>
    /// 完整唯一 Id
    /// </summary>
    public string Id => ProviderId + TypeId + ActualId;

    /// <summary>
    /// 解构函数 (这么翻吗?)
    /// </summary>
    /// <param name="providerId">提供者 Id</param>
    /// <param name="typeId">类型 Id</param>
    /// <param name="actualId">实际 Id</param>
    public void Deconstruct(out string providerId, out string typeId, out string actualId)
    {
        providerId = ProviderId;
        typeId = TypeId;
        actualId = ActualId;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="providerId">提供者 Id</param>
    /// <param name="typeId">类型 Id</param>
    /// <param name="actualId">实际 Id</param>
    public ProvidableItemBase(string providerId, string typeId, string actualId)
    {
        ProviderId = providerId;
        TypeId = typeId;
        ActualId = actualId;
        Name = string.Empty;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="providerId">提供者 Id</param>
    /// <param name="inProviderId">Provider 中的 Id</param>
    public ProvidableItemBase(string providerId, string inProviderId)
    {
        ProviderId = providerId;
        TypeId = InProviderId.Substring(3, 2);
        ActualId = InProviderId.Substring(5);
        Name = string.Empty;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="Id">完整唯一 Id</param>
    public ProvidableItemBase(string Id)
    {
        ProviderId = Id.Substring(0,3);
        TypeId = Id.Substring(3, 2);
        ActualId = Id.Substring(5);
        Name = string.Empty;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public ProvidableItemBase()
    {
        ProviderId = string.Empty;
        TypeId = string.Empty;
        ActualId = string.Empty;
        Name = string.Empty;
    }
}