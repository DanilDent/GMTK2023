using System;

[Serializable]
public struct News
{
    public static News Empty = new();
    public string description;
    public GameTime timeToNews;

    public News(string description, GameTime timeToNews)
    {
        this.description = description;
        this.timeToNews = timeToNews;
    }
}
