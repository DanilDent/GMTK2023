using System;
using UnityEngine;

[Serializable]
public struct GameTime : IEquatable<GameTime>
{
    public int Day => _day - 1;
    public int Hours => _hours;
    public int Minutes => _minutes;

    [SerializeField] private int _day;
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;

    public static GameTime Zero => new(0, new Vector2Int(0, 0));

    public GameTime(int day = 0, int hours = 0, int minutes = 0)
    {
        _day = day;
        _hours = hours;
        _minutes = minutes;
    }

    public GameTime(int day, Vector2Int time)
    {
        _day = day;
        _hours = time.x;
        _minutes = time.y;
    }

    public bool Equals(GameTime other)
    {
        return
            Day.Equals(other.Day) &&
            Hours.Equals(other.Hours) &&
            Minutes.Equals(other.Minutes);
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

    public static bool operator <=(GameTime gameTime1, GameTime gameTime2)
    {
        return GetTotalMinutes(gameTime1) <= GetTotalMinutes(gameTime2);
    }
    public static bool operator >=(GameTime gameTime1, GameTime gameTime2)
    {
        return GetTotalMinutes(gameTime1) >= GetTotalMinutes(gameTime2);
    }

    public static explicit operator int(GameTime gameTime)
    {
        return GetTotalMinutes(gameTime);
    }

    public static GameTime operator +(GameTime t1, GameTime t2)
    {
        int totalMinutesT1 = GetTotalMinutes(t1);
        int totalMinutesT2 = GetTotalMinutes(t2);
        int totalMinutesResult = totalMinutesT1 + totalMinutesT2;

        int resDay = totalMinutesResult / (24 * 60);
        totalMinutesResult -= resDay * 60 * 24;
        int resHours = totalMinutesResult / 60;
        totalMinutesResult -= resHours * 60;
        int resMinutes = totalMinutesResult;

        return new GameTime(resDay, resHours, resMinutes);
    }

    public static GameTime operator -(GameTime t1, GameTime t2)
    {
        int totalMinutesT1 = GetTotalMinutes(t1);
        int totalMinutesT2 = GetTotalMinutes(t2);
        int totalMinutesResult = totalMinutesT1 - totalMinutesT2;
        if (totalMinutesResult < 0)
        {
            return Zero;
        }

        int resDay = totalMinutesResult / (24 * 60);
        totalMinutesResult -= resDay * 24 * 60;
        int resHours = totalMinutesResult / 60;
        totalMinutesResult -= resHours * 60;
        int resMinutes = totalMinutesResult;

        return new GameTime(resDay, resHours, resMinutes);
    }

    public override string ToString()
    {
        return $"Day {Day + 1} {Hours}:{Minutes}";
    }

    public string ToNiceString()
    {
        return $"{_hours.ToString("D2")}:{_minutes.ToString("D2")}";
    }

    private static int GetTotalMinutes(GameTime gameTime)
    {
        return gameTime._day * 24 * 60 + gameTime._hours * 60 + gameTime._minutes;
    }
}