using System.ComponentModel;
using System.Runtime.CompilerServices;
using HyPlayer.Uta.Annotations;

namespace HyPlayer.Uta.AudioService;

public class AudioServiceStatus : INotifyPropertyChanged
{
    private int _volume = 100;

    public int Volume
    {
        get => _volume; 
        set
        {
            if (value == _volume) return;
            _volume = value;
            OnPropertyChanged();
        }
    }

    private PlayStatus _playStatus = PlayStatus.Stopped;
    
    public PlayStatus PlayStatus
    {
        get => _playStatus;
        set
        {
            if (value == _playStatus) return;
            _playStatus = value;
            OnPropertyChanged();
        }
    }

    private bool _buffering = false;
    
    public bool Buffering
    {
        get => _buffering;
        set
        {
            if (value == _buffering) return;
            _buffering = value;
            OnPropertyChanged();
        }
    }

    private double _playSpeed = 1.0;
    
    public double PlaySpeed
    {
        get => _playSpeed;
        set
        {
            _playSpeed = value;
            OnPropertyChanged();
        }
    }

    private TimeSpan _position = TimeSpan.Zero;
    
    public TimeSpan Position
    {
        get => _position;
        set
        {
            if (value == _position) return;
            _position = value;
            OnPropertyChanged();
        }
    }


    /// Property Changed 保留事件
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}