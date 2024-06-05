using Godot;
using System;

public class Sprite : Godot.Sprite
{
    public override void _Ready()
    {
        
    }

    private void _on_Continue_pressed(Node body)
    {
        GetTree().Paused = false;
        GetNode<Control>("/root/Room/Control/EscapeMenu").Hide();
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }
}
