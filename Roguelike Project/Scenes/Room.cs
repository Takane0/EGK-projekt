    using Godot;
using System;

public class Room : Spatial
{
#pragma warning disable 649
    [Export]
    public PackedScene MobScene;
#pragma warning restore 649
    int I = 0;
    public override void _Ready()
    {
        MobSpawn();
        MobSpawn();
        MobSpawn();
    }
    public override void _Process(float delta)
    {
        GD.Randf();
        if(I == 1) { MobSpawn(); I = 3; 
        }
        I++;
    }
    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_accept") && GetNode<Control>("/root/Room/Control/DeathScreen").Visible)
        {
            Single.Set_PlayerCurrentHp(Single.Get_PlayerMaxHp());
            GetTree().ReloadCurrentScene();
        }
    }
    public void _on_Button_pressed()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        GetNode<Control>("/root/Room/Control/EscapeMenu").Hide();
        GetTree().Paused = false;
        Single.Set_PlayerCurrentHp(Single.Get_PlayerMaxHp());
        GetTree().ReloadCurrentScene();
    }
    private void MobSpawn()
    {
        Mob mob = (Mob)MobScene.Instance();

        var mobSpawnLocation = GetNode<PathFollow>("spawnpath/spawnlocation");
        mobSpawnLocation.UnitOffset = Single.get_RandomFloat(0, 100)/100;


        Vector3 playerPosition = GetNode<Player>("Player").Transform.origin;
        mob.Initialize(mobSpawnLocation.Translation, playerPosition);

        AddChild(mob);
    }

}
