using Godot;
using System;

public class PlayerHp : Label
{
    public override void _Ready()
    {

    }
    public override void _Process(float delta)
    {
        this.Text = Single.Get_PlayerCurrentHp() + "/" + Single.Get_PlayerMaxHp();
    }
}
