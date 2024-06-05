using Godot;
using System;

public class GreenHp : ColorRect
{
    public override void _Ready()
    {

    }
    public override void _Process(float delta)
    {
        if (Single.Get_PlayerCurrentHp() / Single.Get_PlayerMaxHp() <= 0.2)
        {
            GetNode<ColorRect>("/root/Room/Control/GreenHp").Hide();
        }
        if (Single.Get_PlayerCurrentHp() / Single.Get_PlayerMaxHp() >= 0.2)
        {
            GetNode<ColorRect>("/root/Room/Control/GreenHp").Show();
        }
    }
}
