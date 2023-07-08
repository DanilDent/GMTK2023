using System;

[Serializable]
public struct GameTime : IEquatable<GameTime>
{
    public int Day;
    public int Hours;
    public int Minutes;

    public bool Equals(GameTime other)
    {
        return
            Day.Equals(other) &&
            Hours.Equals(other) &&
            Minutes.Equals(other);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (obj is GameTime cast)
            return Equals(cast);
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Day.GetHashCode(), Hours.GetHashCode(), Minutes.GetHashCode());
    }

    public static bool operator ==(GameTime t1, GameTime t2)
    {
        return t1.Equals(t2);
    }

    public static bool operator !=(GameTime t1, GameTime t2)
    {
        return !t1.Equals(t2);
    }

    public static bool operator <(GameTime gameTime1, GameTime gameTime2)
    {
        return GetTotalMinutes(gameTime1) < GetTotalMinutes(gameTime2);
    }
    public static bool operator >(GameTime gameTime1, GameTime gameTime2)
    {
        return GetTotalMinutes(gameTime1) > GetTotalMinutes(gameTime2);
    }

    public static GameTime operator +(GameTime t1, GameTime t2)
    {
        int totalMinutesT1 = t1.Day * 24 * 60 + t1.Hours * 60 + t1.Minutes;
        int totalMinutesT2 = t2.Day * 24 * 60 + t2.Hours * 60 + t2.Minutes;
        int totalMinutesResult = totalMinutesT1 + totalMinutesT2;

        int resDay = totalMinutesResult / (24 * 60);
        totalMinutesResult -= resDay;
        int resHours = totalMinutesResult / 60;
        totalMinutesResult -= resHours;
        int resMinutes = totalMinutesResult;

        return new GameTime { Day = resDay, Hours = resHours, Minutes = resMinutes };
    }

    public static GameTime operator -(GameTime t1, GameTime t2)
    {
        int totalMinutesT1 = GetTotalMinutes(t1);
        int totalMinutesT2 = GetTotalMinutes(t2);
        int totalMinutesResult = totalMinutesT1 + totalMinutesT2;

        if (totalMinutesResult < 0)
        {
            UnityEngine.Debug.LogWarning("Warning: you substracting future time from past time");
        }

        int resDay = totalMinutesResult / (24 * 60);
        totalMinutesResult -= resDay;
        int resHours = totalMinutesResult / 60;
        totalMinutesResult -= resHours;
        int resMinutes = totalMinutesResult;

        return new GameTime { Day = resDay, Hours = resHours, Minutes = resMinutes };
    }

    public override string ToString()
    {
        return $"Day {Day} {Hours}:{Minutes}";
    }

    private static int GetTotalMinutes(GameTime gameTime)
    {
        return gameTime.Day * 24 * 60 + gameTime.Hours * 60 + gameTime.Minutes;
    }
}