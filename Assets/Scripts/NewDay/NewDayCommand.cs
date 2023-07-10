using UnityEngine;

public class NewDayCommand
{
    public NewDayCmdType CmdType;
    public Sprite Background;
    public Sprite BackgroundNpc;
    public AudioClip Audio;
    public ImportantNews News;
}

public enum NewDayCmdType
{
    DisplayNews,
    SwitchBackground,
    SwitchMusic
}

public struct ImportantNews
{
    public News News;
    public NewsInfo.NewsType NewsType;
}