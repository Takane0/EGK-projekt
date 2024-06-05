using Godot;
using System;

public class DeathScreen : ColorRect
{
    public override void _Ready()
    {
        GetNode<Control>("/root/Room/Control/DeathScreen").Hide();
    }
}
