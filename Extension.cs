namespace HyPlayer.Uta;

public static class Extension
{
    public static T GetValueOrDefault<T>(this List<T> list, int index, T defaultValue)
    {
        if (list.Count <= index || index < 0)
        {
            return defaultValue;
        }
        return list[index];
    }
}