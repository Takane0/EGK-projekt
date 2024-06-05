using Godot;
using System;

public class Mob : KinematicBody
{
#pragma warning disable 649
    [Export]
    public PackedScene SwordTestScene;
#pragma warning restore 649
    [Export]
    public int MinSpeed { get; set; } = 10;
    [Export]
    public int MaxSpeed { get; set; } = 18;
    public static int Hp = 1;//Convert.ToInt32(Single.get_RandomFloat(1, 3));
    public int counter = 0;
    public bool Agro = false;
    public int RotateCounter = 0;

    private Vector3 _velocity = Vector3.Zero;

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(_velocity);
    }

    public override void _Process(float delta)
    {

    }

    public void Initialize(Vector3 startPosition, Vector3 playerPosition)
    {
        LookAtFromPosition(startPosition, playerPosition, Vector3.Up);
        var randomSpeed = (float)GD.RandRange(MinSpeed, MaxSpeed);
        _velocity = Vector3.Forward * randomSpeed;
        _velocity = _velocity.Rotated(Vector3.Up, Rotation.y);
    }
    public void _on_Agro_body_entered(Node body)
    {
       Agro= true;
    }
    public void _on_Agro_body_exited(Node body)
    {
        Agro= false;
    }
    private void Die()
    {
        SwordTestSpawn();
        _velocity= Vector3.Zero;
        //QueueFree();
    }
    public void _on_Detector_body_entered(Node body)
    {
        Get_Damage();
    }
    public void Get_Damage()
    {
        Mob.Hp = Mob.Hp - Convert.ToInt32(Single.Calc_Dmg(1, 1));
        if (Hp <= 0)
        {
            Die();
        }
    }
    public void SwordTestSpawn()
    {
        SwordTest swordTest = (SwordTest)SwordTestScene.Instance();
        AddChild(swordTest);
    }
}
