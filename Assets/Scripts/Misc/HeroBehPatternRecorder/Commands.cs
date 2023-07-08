using System.Numerics;

public class Command
{
    public CommandType CmdType;
    public Vector3 Vec3;
    public ButtonType BtnType;

}

public enum ButtonType
{
    Next,
    Shop,
    Talk,
    GetQuest,
    Back
}

public enum CommandType
{
    SetCursorPosition,
    MoveCursorTo,
    ClickButtonCommand
}

/* Buttons
 * Next
 * Shop
 * Talk
 * Get Quest
 * Back
 */