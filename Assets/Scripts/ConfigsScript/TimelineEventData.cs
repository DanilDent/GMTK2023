using System;

[Serializable]
public struct TimelineEventData
{
    public TimelineEventType EventType;
    public GameTime GameTime;
    public string Name;
}
