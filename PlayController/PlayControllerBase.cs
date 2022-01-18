namespace HyPlayer.Uta.PlayController;

public abstract class PlayControllerBase
{
    public string Name;
    public string Id;
    public PlayCoreEvents PlayCoreEvents;
    public abstract Task InitializeController(PlayCoreEvents events);
    public abstract Task DisposeController(PlayCoreEvents events);
}