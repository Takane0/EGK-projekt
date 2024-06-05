using Godot;
using System;

public class SwordTest : KinematicBody
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetParent().RemoveChild(this);
        GetNode<Room>(".").AddChild(this);
    }

    private void Die()
    {
        QueueFree();
    }
    public void _on_Detector_body_entered(Node body)
    {
        Get_Damage();
    }
    public void Get_Damage()
    {
            Die();

    }
}
