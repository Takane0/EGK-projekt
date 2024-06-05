using Godot;
using System;

public class MenuButton : Button
{
    public override void _Ready()
    {
        this.Connect("pressed", this, "OnPressed");
    }
    void OnPressed()
    {
        ChangeSceneMenu();
    }
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("ui_focus_next"))
        {
            GetTree().ChangeScene("res://Scenes/Room.tscn");
        }
    }
    public void ChangeSceneMenu()
    {
        GetTree().ChangeScene("res://Scenes/Room.tscn");
    }

}
