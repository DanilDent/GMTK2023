using System.Numerics;

public class Command
{
    public CommandType CmdType;
    public Vector3 Vec3;
    public ButtonType BtnType;

}

public enum ButtonType
{
    Skip,
    Next,
    Shop,
    Talk,
    GetQuest,
    Exit,
    AnsA,
    AnsB,
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