using Godot;
using System;
using System.Collections.Generic;
using static Mob;

public class Player : KinematicBody
{
    Dictionary<string, int> accel_type = new Dictionary<string, int>();

    Spatial head;
    Camera camera;

    // basic variables needed for movement
    [Export]
    float speed = 15.0f;
    [Export]
    float gravity = 20.8f;
    [Export]
    float jump = 7.5f;
    [Export]
    float cam_accel = 40f;
    [Export]
    float mouse_sense = 0.1f;
    [Export]
    float friction = 10.5f;
    [Export]
    int accel;
    public static bool PlayerDmgTakingCooldown = false;
    public Player()
    {
        
    }

    // declaring important nodes
    Vector3 snap;
    Vector3 direction = new Vector3();
    Vector3 velocity = new Vector3();
    Vector3 gravity_vec = new Vector3();
    Vector3 movement = new Vector3();

    // Initially loaded stuff
    public override void _Ready()
    {
        accel_type.Add("default", 7);
        accel_type.Add("air", 1);
        accel = accel_type["default"];
        head = GetNode<Spatial>("Head");
        camera = GetNode<Spatial>("Head").GetChild<Camera>(0);

        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    // formula that allows movement relative to camera
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouseMotion)
        {
            RotateY(Mathf.Deg2Rad(-mouseMotion.Relative.x * mouse_sense));
            head.RotateX(Mathf.Deg2Rad(-mouseMotion.Relative.y * mouse_sense));
            Vector3 rotDeg = head.RotationDegrees;
            rotDeg.x = Mathf.Clamp(rotDeg.x, -89f, 89f);
            head.RotationDegrees = rotDeg;
        }
        if (@event.IsActionPressed("ui_accept") && GetNode<Control>("/root/Room/Control/DeathScreen").Visible)
        {
            Single.Set_PlayerCurrentHp(Single.Get_PlayerMaxHp());
            GetTree().ReloadCurrentScene();
        }
        if (@event.IsActionPressed("ui_cancel") && GetNode<Control>("/root/Room/Control/EscapeMenu").Visible == false)
        {
            GetTree().Paused = true;
            GetNode<Control>("/root/Room/Control/EscapeMenu").Show();
            Input.MouseMode = Input.MouseModeEnum.Confined;
        }
    }
    // processing movement (and some extra stuff(?))

    public void _on_Continue_pressed()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        GetNode<Control>("/root/Room/Control/EscapeMenu").Hide();
        GetTree().Paused = false;
    }
    public void _on_Button2_pressed()
    {
        GetTree().Quit();
    }
    public override void _Process(float delta)
    {
        if (Engine.GetFramesPerSecond() > Engine.IterationsPerSecond)
        {
            camera.SetAsToplevel(true);

            Vector3 Gtrans = head.GlobalTransform.origin;


            var cameraGT = camera.GlobalTransform;
            cameraGT.origin = camera.GlobalTransform.origin.LinearInterpolate(Gtrans, cam_accel * delta);
            camera.GlobalTransform = cameraGT;

            Vector3 camRot = camera.Rotation;
            camRot.y = Rotation.y;
            camRot.x = head.Rotation.x;
            camera.Rotation = camRot;
        }
        else
        {
            camera.SetAsToplevel(false);
            camera.GlobalTransform = head.GlobalTransform;
        }       
    }
    public override void _PhysicsProcess(float delta)
    {
        direction = Vector3.Zero;
        var h_rot = GlobalTransform.basis.GetEuler().y;
        var f_input = Input.GetActionStrength("move_back") - Input.GetActionStrength("move_forward");
        var h_input = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        direction = new Vector3(h_input, 0, f_input).Rotated(Vector3.Up, h_rot).Normalized();

        if (IsOnFloor())
        {
            snap = -GetFloorNormal();
            accel = accel_type["default"];
            gravity_vec = Vector3.Zero;

            // Apply friction
            if (velocity.Length() > 0)
            {
                velocity -= velocity.Normalized() * friction * delta;
                if (velocity.Length() < 0.1)
                {
                    velocity = Vector3.Zero;
                }
            }
        }
        else
        {
            snap = Vector3.Down;
            accel = accel_type["air"];
            gravity_vec += Vector3.Down * gravity * delta;
        }

        if (Input.IsActionJustPressed("movement_jump") && IsOnFloor())
        {
            snap = Vector3.Zero;
            gravity_vec = Vector3.Up * jump;
        }


        velocity = velocity.LinearInterpolate(direction * speed, accel * delta);
        movement = velocity + gravity_vec;

        MoveAndSlideWithSnap(movement, snap, Vector3.Up);

    }
    private void Die()
    {
        GetNode<Control>("/root/Room/Control/DeathScreen").Show();
        QueueFree();
    }
    public void _on_MobDetector_body_entered(Node body)
    {
        Get_Damage();
    }
    public void Get_Damage()
    {
        Single.Set_PlayerCurrentHp(Convert.ToInt32(Single.Get_PlayerCurrentHp()) - Convert.ToInt32(Single.Calc_Dmg(1, 1)));
        if (Convert.ToInt32(Single.Get_PlayerCurrentHp()) <= 0)
        {
            Die();
        }
    }
}
