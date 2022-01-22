using HyPlayer.Uta.MusicProvider;

namespace HyPlayer.Uta.AudioService;

public abstract class AudioServiceBase
{
    public string Id { get; set; }
    public AudioServiceStatus Status { get; protected set; }
    public abstract Task InitializeService(PlayCoreEvents events, AudioServiceStatus audioServiceStatus);
    public abstract Task DisposeServiceAsync(PlayCoreEvents events);
    public abstract Task<AudioTicketBase> GetAudioTicketAsync(MusicMediaSource inputSource);
    public abstract Task<List<AudioTicketBase>> GetAudioTicketListAsync();
    public abstract Task PlayAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract Task PauseAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract Task DisposeAudioTicketAsync(AudioTicketBase audioTicket);
    public abstract void SetMainAudioTicket(AudioTicketBase audioTicket);
}

public enum PlayStatus
{
    Stopped,
    Playing,
    Paused,
    Failed,
    Disposed
}

public abstract class AudioTicketBase
{
    public bool IsDisposed { get; set; }
    public bool IsBuffering { get; set; }
    public PlayStatus Status { get; set; }
    public TimeSpan Position { get; set; }
    public string AudioServiceId { get; }
}