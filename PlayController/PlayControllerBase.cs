namespace HyPlayer.Uta.PlayController;

public abstract class PlayControllerBase
{
    public string Name;
    public string Id;
    public PlayCoreEvents PlayCoreEvents;
    public abstract Task InitializeController(PlayCore playCore);
    public abstract Task DisposeController(PlayCore playCore);
}