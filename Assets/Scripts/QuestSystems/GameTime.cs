using System;
using UnityEngine;

[Serializable]
public struct GameTime
{
    public int Day;
    public Vector2Int Time;

    public GameTime(int day, Vector2Int time)
    {
        Day = day;
        Time = time;
    }

    public override string ToString()
    {
        return $"Day: {Day} Time: {Time.x}:{Time.y}";
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
        return (int)gameTime1 < (int)gameTime2;
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
        return (int)gameTime1 > (int)gameTime2;
    }
    public static bool operator ==(GameTime gameTime1, GameTime gameTime2)
    {
        return gameTime1.Equals(gameTime2);
    }
    public static bool operator !=(GameTime gameTime1, GameTime gameTime2)
    {
        return !gameTime1.Equals(gameTime2);
    }
    public static bool operator <=(GameTime gameTime1, GameTime gameTime2)
    {
        if (gameTime1.Equals(gameTime2))
        {
            return true;
        }
        if (gameTime1.Day < gameTime2.Day)
        {
            return true;
        }
        else if (gameTime1.Day > gameTime2.Day)
        {
            return false;
        }
        return (int)gameTime1 < (int)gameTime2;
    }

    public static bool operator >=(GameTime gameTime1, GameTime gameTime2)
    {
        if (gameTime1.Equals(gameTime2))
        {
            return true;
        }
        if (gameTime1.Day > gameTime2.Day)
        {
            return true;
        }
        else if (gameTime1.Day < gameTime2.Day)
        {
            return false;
        }
        return (int)gameTime1 > (int)gameTime2;
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
        GameTime result = new GameTime()
        {
            Day = gameTime1.Day + gameTime2.Day,
            Time = gameTime1.Time + gameTime2.Time
        };
        var newHours = (result.Time.y + 1) / 60;
        result.Time.y -= newHours * 60;
        result.Time.x += newHours;
        return result;
    }

    public static GameTime operator -(GameTime gameTime1, GameTime gameTime2)
    {
        GameTime result = new GameTime()
        {
            Day = gameTime1.Day - gameTime2.Day,
            Time = gameTime1.Time - gameTime2.Time
        };
        if (result.Day < 0)
        {
            result.Day = 0;
        }
        if (result.Time.y < 0)
        {
            var reducedHours = Mathf.Abs((result.Time.y + 1) / 60) + 1;
            result.Time.y += reducedHours * 60;
            result.Time.x -= reducedHours;
            if (result.Time.x < 0)
            {
                result.Time.x = 0;
                result.Time.y = 0;
            }
        }

        return result;
    }
}
