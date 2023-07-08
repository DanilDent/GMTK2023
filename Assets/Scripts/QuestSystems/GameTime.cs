using UnityEngine;

public struct GameTime
{
    public int Day;
    public Vector2Int Time;

    public GameTime(int day, Vector2Int time)
    {
        Day = day;
        Time = time;
    }

    public static explicit operator int(GameTime gameTime)
    {
        return gameTime.Time.x * 60 + gameTime.Time.y;
    }

    public static bool operator <(GameTime gameTime1, GameTime gameTime2)
    {
        if (gameTime1.Day < gameTime2.Day)
        {
            return true;
        }
        else if (gameTime1.Day > gameTime2.Day)
        {
            return false;
        }
        return (int)gameTime1 < (int)gameTime2.Day;
    }
    public static bool operator >(GameTime gameTime1, GameTime gameTime2)
    {
        if (gameTime1.Day > gameTime2.Day)
        {
            return true;
        }
        else if (gameTime1.Day < gameTime2.Day)
        {
            return false;
        }
        return (int)gameTime1 > (int)gameTime2.Day;
    }

    public override bool Equals(object obj)
    {
        if (obj is GameTime other)
        {
            if (other.Day != Day)
            {
                return false;
            }
            return Time.Equals(other.Time);
        }
        else
        {
            return base.Equals(obj);
        }
    }

    public static GameTime operator +(GameTime gameTime1, GameTime gameTime2)
    {
        return new GameTime()
        {
            Day = gameTime1.Day + gameTime2.Day,
            Time = gameTime1.Time + gameTime2.Time
        };
    }

    public static GameTime operator -(GameTime gameTime1, GameTime gameTime2)
    {
        return new GameTime()
        {
            Day = gameTime1.Day - gameTime2.Day,
            Time = gameTime1.Time - gameTime2.Time
        };
    }
}
