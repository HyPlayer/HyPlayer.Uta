namespace HyPlayer.Uta.AudioService;

/// <summary>
/// 允许后台播放
/// </summary>
public interface IBackgroundPlayable
{
    /// <summary>
    /// 进入后台, 可在此处回收不必要的内存等东西.
    /// </summary>
    /// <returns></returns>
    public Task EnteringBackground();

    /// <summary>
    /// 进入前台
    /// 注意: 在一开始的时候不会调用此函数.仅在进入后台之后再切回前台时调用
    /// </summary>
    /// <returns></returns>
    public Task EnteringForeground();
}

/// <summary>
/// 可以更改输出设备
/// </summary>
public interface IOutputDeviceChangeable
{
    /// <summary>
    /// 获取可输出设备列表
    /// </summary>
    /// <returns></returns>
    public Task<List<OutputDeviceBase>> GetOutputDevices();
    
    /// <summary>
    /// 设置输出设备
    /// </summary>
    /// <param name="device">设备信息</param>
    /// <returns></returns>
    public Task SetOutputDevices(OutputDeviceBase device);
}

public abstract class OutputDeviceBase
{
    public string Name;
}

/// <summary>
/// 允许更改播放更改播放倍速 (x1.0)
/// </summary>
public interface IPlaybackRateChangeable
{
    /// <summary>
    /// 更改 AudioService 播放倍速
    /// </summary>
    /// <param name="playbackSpeed">目标速度(x1.0)</param>
    /// <returns></returns>
    public Task ChangePlaybackSpeed(double playbackSpeed);
}

/// <summary>
/// 允许更改播放输出的音量
/// </summary>
public interface IVolumeChangeable
{
    /// <summary>
    /// 更改 AudioService 总音量
    /// </summary>
    /// <param name="volume">目标音量 0~100</param>
    /// <returns></returns>
    public Task ChangeVolume(int volume);
}