using Godot;
using System;

public class RedHp : ColorRect
{
    public override void _Ready()
    {
        
    }
    public override void _Process(float delta)
    {
        if (Single.Get_PlayerCurrentHp() / Single.Get_PlayerMaxHp() <= 0.2)
        {
            GetNode<ColorRect>("/root/Room/Control/RedHp").Show();
        }
        if (Single.Get_PlayerCurrentHp() / Single.Get_PlayerMaxHp() >= 0.2)
        {
            GetNode<ColorRect>("/root/Room/Control/RedHp").Hide();
        }
    }
}
