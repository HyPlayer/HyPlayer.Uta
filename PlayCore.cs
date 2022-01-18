using System.ComponentModel;
using System.Runtime.CompilerServices;
using HyPlayer.Uta.Annotations;
using HyPlayer.Uta.AudioService;
using HyPlayer.Uta.MusicProvider;
using HyPlayer.Uta.PlayController;
using HyPlayer.Uta.ProvidableItem;
using HyPlayer.Uta.ProvidableItem.SongContainer;

namespace HyPlayer.Uta;

public class PlayCore : INotifyPropertyChanged
{
    public PlayCore()
    {
    }

    #region Basic Information

    /// <summary>
    /// 音乐提供者列表
    /// </summary>
    public readonly Dictionary<string, MusicProviderBase> MusicProviders = new();

    /// <summary>
    /// 音频服务列表
    /// </summary>
    public readonly Dictionary<string, AudioServiceBase> AudioServiceBases = new();

    /// <summary>
    /// 播放控制器列表
    /// </summary>
    public readonly Dictionary<string, PlayControllerBase> PlayControllers = new();

    /// <summary>
    /// 当前播放服务
    /// </summary>
    public AudioServiceBase AudioService;

    /// <summary>
    /// 总事件器
    /// </summary>
    public readonly PlayCoreEvents EventListener = new();

    /// <summary>
    /// 播放列表
    /// </summary>
    public readonly List<SingleSongBase> PlayList = new();

    /// <summary>
    /// 播放来源
    /// </summary>
    public SongContainerBase? PlayListSource
    {
        get => _playListSource;
        set
        {
            _playListSource = value;
            OnPropertyChanged();
        }
    }

    private SongContainerBase? _playListSource;

    private int _nowPlayingIndex = -1;

    /// <summary>
    /// 当前播放指针
    /// </summary>
    public int NowPlayingIndex
    {
        get => _nowPlayingIndex;
        set
        {
            if (value == _nowPlayingIndex) return;
            _nowPlayingIndex = value;
            OnPropertyChanged();
        }
    }

    private PlayRollMode _playRollingMode = PlayRollMode.DefaultRoll;

    /// <summary>
    /// 播放模式
    /// </summary>
    public PlayRollMode PlayRollingMode
    {
        get => _playRollingMode;
        set
        {
            _playRollingMode = value;
            OnPropertyChanged();
        }
    }

    private byte _playListChangedIndicator;

    /// <summary>
    /// 播放列表变化指示器
    /// 这是一个十分巧妙的指示器. 在播放列表发生变化时, 此属性将会更改.
    /// 可以通过 Converter 将此属性的 Bind 转换为列表
    /// </summary>
    public byte PlayListChangedIndicator
    {
        get => _playListChangedIndicator;
        private set
        {
            _playListChangedIndicator = value;
            OnPropertyChanged();
        }
    }

    private SingleSongBase _nowPlayingSong;

    /// <summary>
    /// 当前播放歌曲
    /// </summary>
    public SingleSongBase NowPlayingSong
    {
        get => _nowPlayingSong;
        private set
        {
            _nowPlayingSong = value;
            OnPropertyChanged();
        }
    }

    public AudioTicketBase NowPlayingTicket;

    /// <summary>
    /// 播放设置 (废弃)
    /// </summary>
    [Obsolete("此项已经废弃")] public readonly object PlayCoreSettings = new();

    /// <summary>
    /// 随机数生成器
    /// </summary>
    public readonly Random RandomGenerator = new();

    #endregion

    #region Basic Methods

    /// <summary>
    /// 注册音频服务
    /// </summary>
    /// <param name="audioServiceBase">音频服务</param>
    public void RegisterAudioService(AudioServiceBase audioServiceBase)
    {
        AudioServiceBases[audioServiceBase.Id] = audioServiceBase;
    }

    /// <summary>
    /// 选中音频服务
    /// </summary>
    /// <param name="audioServiceId">音频服务 Id</param>
    public async Task UseAudioServiceAsync(string audioServiceId)
    {
        if (AudioService != null)
            _ = AudioService.DisposeServiceAsync(EventListener);
        await AudioServiceBases[audioServiceId].InitializeService(EventListener);
        AudioService = AudioServiceBases[audioServiceId];
    }

    /// <summary>
    /// 注册播放控制器
    /// </summary>
    /// <param name="playControllerBase">播放控制器</param>
    public Task RegisterPlayControllerAsync(PlayControllerBase playControllerBase)
    {
        PlayControllers[playControllerBase.Id] = playControllerBase;
        return playControllerBase.InitializeController(EventListener);
    }

    /// <summary>
    /// 销毁播放控制器
    /// </summary>
    /// <param name="playControllerId">播放控制器 Id</param>
    public void DisposePlayControllerById(string playControllerId)
    {
        _ = PlayControllers[playControllerId].DisposeController(EventListener);
        PlayControllers.Remove(playControllerId);
    }

    #endregion

    #region Play Control Function

    /// <summary>
    /// 添加一个音乐单曲到播放列表
    /// 要添加多个单曲请使用 AppendPlayItemRange
    /// </summary>
    /// <param name="item">音乐单曲</param>
    public async void AppendSingleSong(SingleSongBase item)
    {
        var args = new AddPlayItemsEventArgs(this, item, PlayList.Count);
        await EventListener.RaisePlayItemAddBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.Add(item);
        PlayListChangedIndicator = 0;
        await EventListener.RaisePlayItemAddAfterEvent(args);
    }

    /// <summary>
    ///  添加一个音乐单曲到播放列表指定位置
    ///  要添加多个单曲请使用 InsertPlayItemRange
    /// </summary>
    /// <param name="item">音乐单曲</param>
    /// <param name="index">位置</param>
    public async void InsertSingleSong(SingleSongBase item, int index)
    {
        var args = new AddPlayItemsEventArgs(this, item, index);
        await EventListener.RaisePlayItemAddBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.Insert(index, item);
        PlayListChangedIndicator = 0;
        await EventListener.RaisePlayItemAddAfterEvent(args);
    }

    /// <summary>
    ///  添加音乐单曲下一首播放
    /// </summary>
    /// <param name="item">音乐单曲</param>
    public async void AppendSingleSongNext(SingleSongBase item)
    {
        int index = Math.Max(NowPlayingIndex + 1, 0);
        var args = new AddPlayItemsEventArgs(this, item, index);
        await EventListener.RaisePlayItemAddBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.Insert(index, item);
        PlayListChangedIndicator = 0;
        await EventListener.RaisePlayItemAddAfterEvent(args);
    }

    /// <summary>
    /// 添加多个音乐单曲到播放列表
    /// </summary>
    /// <param name="items">音乐列表</param>
    public async void AppendSingleSongRange(List<SingleSongBase> items)
    {
        var args = new AddPlayItemsEventArgs(this, items, PlayList.Count);
        await EventListener.RaisePlayItemAddBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.AddRange(items);
        PlayListChangedIndicator = 0;
        await EventListener.RaisePlayItemAddAfterEvent(args);
    }

    /// <summary>
    /// 添加多个音乐单曲到播放列表指定位置
    /// </summary>
    /// <param name="items">音乐单曲</param>
    /// <param name="index">位置</param>
    public async void InsertSingleSongRange(List<SingleSongBase> items, int index)
    {
        var args = new AddPlayItemsEventArgs(this, items, index);
        await EventListener.RaisePlayItemAddBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.InsertRange(index, items);
        PlayListChangedIndicator = 0;
        await EventListener.RaisePlayItemAddAfterEvent(args);
    }

    /// <summary>
    /// 删除指定位置的音乐单曲
    /// </summary>
    /// <param name="index">位置</param>
    public async void RemoveSingleSongAt(int index)
    {
        if (index < 0 || index >= PlayList.Count) return;
        if (PlayList.Count - 1 == 0)
        {
            RemoveAllSong();
            return;
        }

        if (index == NowPlayingIndex)
        {
            var oldItem = PlayList[index];
            PlayList.RemoveAt(index);
            MovePointerTo(NowPlayingIndex);
        }
        else if (index < NowPlayingIndex)
        {
            //需要将序号向前挪动
            NowPlayingIndex -= 1;
        }

        var args = new RemovePlayItemEventArgs(this, PlayList[index], index);
        await EventListener.RaiseRemovePlayItemBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.RemoveAt(index);
        PlayListChangedIndicator = 0;
        await EventListener.RaiseRemovePlayItemAfterEvent(args);
    }

    /// <summary>
    /// 删除播放列表中的所有音乐单曲
    /// </summary>
    public async void RemoveAllSong()
    {
        var args = new RemoveAllItemsEventArgs(this);
        await EventListener.RaiseRemoveAllItemsBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayList.Clear();
        PlayListChangedIndicator = 0;
        await EventListener.RaiseRemoveAllItemsAfterEvent(args);
    }

    /// <summary>
    /// 获取下一首歌曲的索引 ID
    /// </summary>
    /// <returns>下一首歌的序号</returns>
    public int GetNextSongIndex()
    {
        // 请注意 Shuffle 的情况下 Pointer 可能会变动
        var retPointer = NowPlayingIndex;
        if (PlayList.Count == 0) return -1; // 没有歌曲的话直接切换为不播放
        switch (PlayRollingMode)
        {
            case PlayRollMode.DefaultRoll:
                //正常Roll的话,id++
                if (NowPlayingIndex + 1 >= PlayList.Count)
                    retPointer = 0;
                else
                    retPointer++;
                break;
            case PlayRollMode.SinglePlay:
                retPointer = NowPlayingIndex;
                break;
            case PlayRollMode.Shuffled:
                retPointer = RandomGenerator.Next(PlayList.Count - 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return retPointer;
    }

    /// <summary>
    /// 移动当前播放指针到指定位置
    /// </summary>
    /// <param name="index">位置</param>
    public async void MovePointerTo(int index)
    {
        if (index < 0 || index >= PlayList.Count) return;
        var args = new ChangePlayItemEventArgs(this, PlayList[NowPlayingIndex], PlayList[index], NowPlayingIndex,
            index);
        await EventListener.RaiseChangePlayItemBeforeEvent(args);
        if (args.BreakEvent) return;
        NowPlayingIndex = args.NewIndex;
        await EventListener.RaiseChangePlayItemAfterEvent(args);
    }

    /// <summary>
    /// 移动指针到下一首歌曲
    /// </summary>
    public async void MoveNext()
    {
        // Progressive 在已加载完项播放完后需要继续加载
        if (NowPlayingIndex == PlayList.Count - 1 &&
            PlayListSource is ProgressiveSongContainer progressiveSongContainer)
            AppendSingleSongRange(await progressiveSongContainer.GetNextItems());
        MovePointerTo(GetNextSongIndex());
    }

    /// <summary>
    /// 移动指针到上一首歌曲
    /// </summary>
    public async void MovePrevious()
    {
        if (PlayList.Count == 0) return;
        if (NowPlayingIndex - 1 < 0)
            MovePointerTo(PlayList.Count - 1);
        else
            MovePointerTo(NowPlayingIndex - 1);
    }
    
    /// <summary>
    /// 替换当前播放来源
    /// </summary>
    /// <param name="newSource">新的播放来源</param>
    public async void ReplacePlayListSource(SongContainerBase newSource)
    {
        var args = new ChangePlaySourceEventArgs(this, PlayListSource, newSource);
        await EventListener.RaiseChangePlaySourceBeforeEvent(args);
        if (args.BreakEvent) return;
        PlayListSource = newSource;
        await EventListener.RaiseChangePlaySourceAfterEvent(args);
    }
    
    /// <summary>
    /// 加载当前的播放源
    /// </summary>
    public async void LoadPlaySource()
    {
        PlayList.Clear();
        if (PlayListSource is LinerSongContainerBase linerSongContainerBase)
        {
            var items = await linerSongContainerBase.GetContainerItems();
            AppendSingleSongRange(items);
        }
        else if (PlayListSource is ProgressiveSongContainer progressiveSongContainer)
        {
            var items = await progressiveSongContainer.GetNextItems();
            AppendSingleSongRange(items);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 加载当前播放歌曲
    /// </summary>
    public async Task LoadNowPlayingItemMedia()
    {
        var args = new LoadNowPlayingItemMediaEventArgs(this, PlayList[NowPlayingIndex]);
        await EventListener.RaiseLoadNowPlayingItemMediaBeforeEvent(args);
        if (args.BreakEvent) return;
        if (AudioService is IMediaSourceProvidable mediaSourceProvidable)
        {
            var mediaSource = await mediaSourceProvidable.GetMediaSource(PlayList[NowPlayingIndex]);
            NowPlayingTicket = await AudioService.GetAudioTicketAsync(mediaSource);
        }
    }
    
    /// <summary>
    /// 播放当前歌曲
    /// </summary>
    public async Task Play()
    {
        var args = new AudioTicketOperationEventArgs(this,NowPlayingTicket);
        await EventListener.RaisePlayBeforeEvent(args);
        if (args.BreakEvent) return;
        await AudioService.PlayAudioTicketAsync(NowPlayingTicket);
        await EventListener.RaisePlayAfterEvent(args);
    }

    /// <summary>
    /// 暂停当前歌曲
    /// </summary>
    public async Task Pause()
    {
        var args = new AudioTicketOperationEventArgs(this,NowPlayingTicket);
        await EventListener.RaisePauseBeforeEvent(args);
        if (args.BreakEvent) return;
        await AudioService.PauseAudioTicketAsync(NowPlayingTicket);
        await EventListener.RaisePauseAfterEvent(args);
    }
    
    /// <summary>
    /// 停止当前歌曲
    /// </summary>
    public async Task Stop()
    {
        var args = new AudioTicketOperationEventArgs(this,NowPlayingTicket);
        await EventListener.RaiseStopBeforeEvent(args);
        if (args.BreakEvent) return;
        await AudioService.DisposeAudioTicketAsync(NowPlayingTicket);
        await EventListener.RaiseStopAfterEvent(args);
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

public enum PlayRollMode
{
    DefaultRoll,
    SinglePlay,
    Shuffled
}