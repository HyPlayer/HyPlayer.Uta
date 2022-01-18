namespace HyPlayer.Uta.AudioService;

public abstract class AudioServiceBase
{
    public string Id;
    public PlayStatus PlayStatus;
    public bool IsLoading;
    public TimeSpan Duration;
    public TimeSpan Position;
    public int Volume;
    public double PlaybackSpeed;

    public abstract Task InitializeService(PlayCoreEvents events);
    public abstract Task DisposeServiceAsync(PlayCoreEvents events);
    public abstract Task<AudioTicketBase> GetAudioTicketAsync(object mediaSource);
    public abstract Task<List<AudioTicketBase>> GetAudioTicketListAsync();
    public abstract Task PlayAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract Task PauseAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract Task DisposeAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract Task SeekAudioTicketAsync(AudioTicketBase audioTicket, TimeSpan position);
}

public enum PlayStatus
{
    Stopped,
    Playing,
    Paused,
    Failed
}

public abstract class AudioTicketBase : IDisposable
{
    public bool IsDisposed;
    public string AudioServiceId;

    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
        Dispose(true);
        IsDisposed = true;
        GC.SuppressFinalize(this);
    }
}