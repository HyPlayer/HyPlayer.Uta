using HyPlayer.Uta.Annotations;
using HyPlayer.Uta.AudioService;
using HyPlayer.Uta.ProvidableItem;
using HyPlayer.Uta.ProvidableItem.SongContainer;

namespace HyPlayer.Uta;

/// <summary>
/// The Events Collection
/// Please note that the events are not thread safe
/// and don't abuse before events.
///
/// Events with suffix "Do" is used to control the PlayCore
/// other is used by PlayCore to other things (Like PlayController)
/// </summary>
public class PlayCoreEvents
{
    #region 到 PlayCore 的事件

    /// <summary>
    /// 让播放器播放下一首歌曲
    /// </summary>
    public event DoPlayEvent OnDoPlay;

    public delegate Task DoPlayEvent(AudioTicketBase? audioTicketBase = null);

    public Task RaiseDoPlayEvent(AudioTicketBase? audioTicketBase = null)
    {
        return OnDoPlay?.Invoke(audioTicketBase) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 让播放器暂停
    /// </summary>
    public event DoPauseEvent OnDoPause;

    public delegate Task DoPauseEvent(AudioTicketBase? audioTicketBase = null);

    public Task RaiseDoPauseEvent(AudioTicketBase? audioTicketBase = null)
    {
        return OnDoPause?.Invoke(audioTicketBase) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 让播放器停止
    /// </summary>
    public event DoStopEvent OnDoStop;

    public delegate Task DoStopEvent(AudioTicketBase? audioTicketBase = null);

    public Task RaiseDoStopEvent(AudioTicketBase? audioTicketBase = null)
    {
        return OnDoStop?.Invoke(audioTicketBase) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 更改当前播放的进度
    /// </summary>
    public event DoSeekEvent OnDoSeek;

    public delegate Task DoSeekEvent(TimeSpan position, AudioTicketBase? audioTicketBase = null);

    public Task RaiseDoSeekEvent(TimeSpan position, AudioTicketBase? audioTicketBase = null)
    {
        return OnDoSeek?.Invoke(position, audioTicketBase) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 修改 Ticket 的音量
    /// </summary>
    public event ChangeTicketVolumeEvent OnChangeTicketVolume;

    public delegate Task ChangeTicketVolumeEvent(int volume, AudioTicketBase? audioTicketBase = null);

    public Task RaiseChangeTicketVolumeEvent(int volume, AudioTicketBase? audioTicketBase = null)
    {
        return OnChangeTicketVolume?.Invoke(volume, audioTicketBase) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 获取下一首的播放 Ticket
    /// </summary>
    public event DoGetNextTicketEvent OnDoGetNextTicket;

    public delegate Task<AudioTicketBase?> DoGetNextTicketEvent(object mediaSource);

    public Task<AudioTicketBase?> RaiseDoGetNextTicketEvent(object mediaSource)
    {
        return OnDoGetNextTicket?.Invoke(mediaSource) ?? Task.FromResult<AudioTicketBase?>(null);
    }

    /// <summary>
    /// 移动到下一个
    /// </summary>
    public event DoMoveToEvent OnDoMoveTo;

    public delegate Task DoMoveToEvent(int index);

    public Task RaiseDoMoveToEvent(int index)
    {
        return OnDoMoveTo?.Invoke(index) ?? Task.CompletedTask;
    }

    /// <summary>
    /// 更改总输出音量
    /// </summary>
    public event DoChangeVolumeEvent OnDoChangeVolume;

    public delegate Task DoChangeVolumeEvent(int volume);

    public Task RaiseDoChangeVolumeEvent(int volume)
    {
        return OnDoChangeVolume?.Invoke(volume) ?? Task.CompletedTask;
    }

    #endregion

    #region 到 PlayController 的事件

    public event PlayBeforeEvent OnPlayBefore;

    public delegate Task PlayBeforeEvent(AudioTicketOperationEventArgs args);

    public Task RaisePlayBeforeEvent(AudioTicketOperationEventArgs args)
    {
        return OnPlayBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event PlayAfterEvent OnPlayAfter;

    public delegate Task PlayAfterEvent(AudioTicketOperationEventArgs args);

    public Task RaisePlayAfterEvent(AudioTicketOperationEventArgs args)
    {
        return OnPlayAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event PauseBeforeEvent OnPauseBefore;

    public delegate Task PauseBeforeEvent(AudioTicketOperationEventArgs args);

    public Task RaisePauseBeforeEvent(AudioTicketOperationEventArgs args)
    {
        return OnPauseBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event PauseAfterEvent OnPauseAfter;

    public delegate Task PauseAfterEvent(AudioTicketOperationEventArgs args);

    public Task RaisePauseAfterEvent(AudioTicketOperationEventArgs args)
    {
        return OnPauseAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event StopBeforeEvent OnStopBefore;

    public delegate Task StopBeforeEvent(AudioTicketOperationEventArgs args);

    public Task RaiseStopBeforeEvent(AudioTicketOperationEventArgs args)
    {
        return OnStopBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event StopAfterEvent OnStopAfter;

    public delegate Task StopAfterEvent(AudioTicketOperationEventArgs args);

    public Task RaiseStopAfterEvent(AudioTicketOperationEventArgs args)
    {
        return OnStopAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event FailedBeforeEvent OnFailedBefore;

    public delegate Task FailedBeforeEvent(AudioTicketOperationEventArgs args);

    public Task RaiseFailedBeforeEvent(AudioTicketOperationEventArgs args)
    {
        return OnFailedBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event FailedAfterEvent OnFailedAfter;

    public delegate Task FailedAfterEvent(AudioTicketOperationEventArgs args);

    public Task RaiseFailedAfterEvent(AudioTicketOperationEventArgs args)
    {
        return OnFailedAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePlayItemBeforeEvent OnChangePlayItemBefore;

    public delegate Task ChangePlayItemBeforeEvent(ChangePlayItemEventArgs args);

    public Task RaiseChangePlayItemBeforeEvent(ChangePlayItemEventArgs args)
    {
        return OnChangePlayItemBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePlayItemAfterEvent OnChangePlayItemAfter;

    public delegate Task ChangePlayItemAfterEvent(ChangePlayItemEventArgs args);

    public Task RaiseChangePlayItemAfterEvent(ChangePlayItemEventArgs args)
    {
        return OnChangePlayItemAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePositionBeforeEvent OnChangePositionBefore;

    public delegate Task ChangePositionBeforeEvent(ChangePositionEventArgs args);

    public Task RaiseChangePositionBeforeEvent(ChangePositionEventArgs args)
    {
        return OnChangePositionBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePositionAfterEvent OnChangePositionAfter;

    public delegate Task ChangePositionAfterEvent(ChangePositionEventArgs args);

    public Task RaiseChangePositionAfterEvent(ChangePositionEventArgs args)
    {
        return OnChangePositionAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangeVolumeBeforeEvent OnChangeVolumeBefore;

    public delegate Task ChangeVolumeBeforeEvent(ChangeVolumeEventArgs args);

    public Task RaiseChangeVolumeBeforeEvent(ChangeVolumeEventArgs args)
    {
        return OnChangeVolumeBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangeVolumeAfterEvent OnChangeVolumeAfter;

    public delegate Task ChangeVolumeAfterEvent(ChangeVolumeEventArgs args);

    public Task RaiseChangeVolumeAfterEvent(ChangeVolumeEventArgs args)
    {
        return OnChangeVolumeAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event PlayItemAddBeforeEvent OnPlayItemAddBefore;

    public delegate Task PlayItemAddBeforeEvent(AddPlayItemsEventArgs args);

    public Task RaisePlayItemAddBeforeEvent(AddPlayItemsEventArgs args)
    {
        return OnPlayItemAddBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event PlayItemAddAfterEvent OnPlayItemAddAfter;

    public delegate Task PlayItemAddAfterEvent(AddPlayItemsEventArgs args);

    public Task RaisePlayItemAddAfterEvent(AddPlayItemsEventArgs args)
    {
        return OnPlayItemAddAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event RemovePlayItemEventBeforeEvent OnRemovePlayItemBefore;

    public delegate Task RemovePlayItemEventBeforeEvent(RemovePlayItemEventArgs args);

    public Task RaiseRemovePlayItemBeforeEvent(RemovePlayItemEventArgs args)
    {
        return OnRemovePlayItemBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event RemovePlayItemEventAfterEvent OnRemovePlayItemAfter;

    public delegate Task RemovePlayItemEventAfterEvent(RemovePlayItemEventArgs args);

    public Task RaiseRemovePlayItemAfterEvent(RemovePlayItemEventArgs args)
    {
        return OnRemovePlayItemAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event RemoveAllItemsEventBeforeEvent OnRemoveAllItemsBefore;

    public delegate Task RemoveAllItemsEventBeforeEvent(RemoveAllItemsEventArgs args);

    public Task RaiseRemoveAllItemsBeforeEvent(RemoveAllItemsEventArgs args)
    {
        return OnRemoveAllItemsBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event RemoveAllItemsEventAfterEvent OnRemoveAllItemsAfter;

    public delegate Task RemoveAllItemsEventAfterEvent(RemoveAllItemsEventArgs args);

    public Task RaiseRemoveAllItemsAfterEvent(RemoveAllItemsEventArgs args)
    {
        return OnRemoveAllItemsAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePlaySourceEventBeforeEvent OnChangePlaySourceBefore;

    public delegate Task ChangePlaySourceEventBeforeEvent(ChangePlaySourceEventArgs args);

    public Task RaiseChangePlaySourceBeforeEvent(ChangePlaySourceEventArgs args)
    {
        return OnChangePlaySourceBefore?.Invoke(args) ?? Task.CompletedTask;
    }

    public event ChangePlaySourceEventAfterEvent OnChangePlaySourceAfter;

    public delegate Task ChangePlaySourceEventAfterEvent(ChangePlaySourceEventArgs args);

    public Task RaiseChangePlaySourceAfterEvent(ChangePlaySourceEventArgs args)
    {
        return OnChangePlaySourceAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    public event LoadNowPlayingItemMediaBeforeEvent OnLoadNowPlayingItemMediaBefore;
    
    public delegate Task LoadNowPlayingItemMediaBeforeEvent(LoadNowPlayingItemMediaEventArgs args);
    
    public Task RaiseLoadNowPlayingItemMediaBeforeEvent(LoadNowPlayingItemMediaEventArgs args)
    {
        return OnLoadNowPlayingItemMediaBefore?.Invoke(args) ?? Task.CompletedTask;
    }
    
    public event LoadNowPlayingItemMediaAfterEvent OnLoadNowPlayingItemMediaAfter;
    
    public delegate Task LoadNowPlayingItemMediaAfterEvent(LoadNowPlayingItemMediaEventArgs args);
    
    public Task RaiseLoadNowPlayingItemMediaAfterEvent(LoadNowPlayingItemMediaEventArgs args)
    {
        return OnLoadNowPlayingItemMediaAfter?.Invoke(args) ?? Task.CompletedTask;
    }

    #endregion
}

public class AudioTicketOperationEventArgs : EventArgs
{
    public AudioTicketOperationEventArgs(object sender, AudioTicketBase audioTicket)
    {
        Sender = sender;
        AudioTicket = audioTicket;
    }

    public object Sender { get; }
    public AudioTicketBase AudioTicket { get; }
    public bool BreakEvent = true;
}

public class ChangePlayItemEventArgs : EventArgs
{
    public ChangePlayItemEventArgs(object sender, SingleSongBase oldSong, SingleSongBase newSong, int oldIndex,
        int newIndex)
    {
        Sender = sender;
        OldSong = oldSong;
        NewSong = newSong;
        OldIndex = oldIndex;
        NewIndex = newIndex;
    }

    public object Sender { get; }
    public SingleSongBase OldSong { get; }
    public SingleSongBase NewSong { get; }
    public int OldIndex { get; }
    public int NewIndex { get; set; }

    public bool BreakEvent = true;
}

public class ChangePositionEventArgs : EventArgs
{
    public ChangePositionEventArgs(object sender, TimeSpan position)
    {
        Sender = sender;
        Position = position;
    }

    public object Sender { get; }
    public TimeSpan Position { get; }

    public bool BreakEvent = true;
}

public class ChangeVolumeEventArgs : EventArgs
{
    public ChangeVolumeEventArgs(object sender, int volume)
    {
        Sender = sender;
        Volume = volume;
    }

    public object Sender { get; }
    public int Volume { get; }

    public bool BreakEvent = true;
}

public class AddPlayItemsEventArgs : EventArgs
{
    public AddPlayItemsEventArgs(object sender, SingleSongBase addItem, int index)
    {
        Sender = sender;
        AddingItems = new List<SingleSongBase> { addItem };
        Index = index;
    }

    public AddPlayItemsEventArgs(object sender, List<SingleSongBase> addItems, int index)
    {
        Sender = sender;
        AddingItems = addItems;
        Index = index;
    }

    public object Sender { get; }
    public List<SingleSongBase> AddingItems { get; }

    public int Index { get; set; }

    public bool BreakEvent = true;
}

public class RemovePlayItemEventArgs : EventArgs
{
    public RemovePlayItemEventArgs(object sender, SingleSongBase removeItem, int index)
    {
        Sender = sender;
        RemovingItems = new List<SingleSongBase> { removeItem };
        Index = index;
    }

    public RemovePlayItemEventArgs(object sender, List<SingleSongBase> removeItems, int index)
    {
        Sender = sender;
        RemovingItems = removeItems;
        Index = index;
    }

    public object Sender { get; }
    public List<SingleSongBase> RemovingItems { get; }

    public int Index { get; set; }

    public bool BreakEvent = true;
}

public class RemoveAllItemsEventArgs : EventArgs
{
    public RemoveAllItemsEventArgs(object sender)
    {
        Sender = sender;
    }

    public object Sender { get; }

    public bool BreakEvent = true;
}

public class ChangePlaySourceEventArgs : EventArgs
{
    public ChangePlaySourceEventArgs(object sender, SongContainerBase? oldPlaySource, SongContainerBase newPlaySource)
    {
        Sender = sender;
        OldPlaySource = oldPlaySource;
        NewPlaySource = newPlaySource;
    }

    public object Sender { get; }
    public SongContainerBase? OldPlaySource { get; }
    public SongContainerBase NewPlaySource { get; }

    public bool BreakEvent = true;
}

public class LoadNowPlayingItemMediaEventArgs : EventArgs
{
    public LoadNowPlayingItemMediaEventArgs(object sender, SingleSongBase song)
    {
        Sender = sender;
        Song = song;
    }

    public object Sender { get; }
    public SingleSongBase Song { get; }

    public bool BreakEvent = true;
}