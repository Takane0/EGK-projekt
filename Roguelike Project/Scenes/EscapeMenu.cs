using Godot;
using System;

public class EscapeMenu : ColorRect
{
    public override void _Ready()
    {
        GetNode<Control>("/root/Room/Control/EscapeMenu").Hide();
    }
}
