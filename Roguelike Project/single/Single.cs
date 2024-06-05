using Godot;
using System;

public class Single : Node
{
    private string PlayerName;
    private static double PlayerCurrentHp = 51;
    private static double PlayerMaxHp = 100;
    private double PlayerCurrentMp;
    private double PlayerMaxMp;
    private double PlayerExp;
    private double PalyerGold;
    private double PlayerArmor;
    private double PlayerDamageReduction;
    private double PlayerLuck;
    private double PlayerLifeSteal;
    private double PlayerCritRate;
    private double PlayerCritDamage;
    private double PlayerSkillpoints;
    private double WorldLevel;
    private double WorldDifficulty;
    private string[] PlayerInv = new string[29];
    public override void _Ready()
    {

    }
    public void Set_PlayerName(string Set_PlayerName)
    {PlayerName = Set_PlayerName;}
    public string Get_PlayerName()
    { return PlayerName; }
    public static void Set_PlayerCurrentHp(double Set_PlayerCurrentHp)
    { PlayerCurrentHp = Set_PlayerCurrentHp;
    }
    public static double Get_PlayerCurrentHp()
    { return PlayerCurrentHp; }
    public void Set_PlayerMaxHp(double Set_PlayerMaxHp)
    { PlayerMaxHp = Set_PlayerMaxHp; }
    public static double Get_PlayerMaxHp()
    { return PlayerMaxHp; }
    public void Set_PlayerCurrentMp(double Set_PlayerCurrentMp)
    { PlayerCurrentMp = Set_PlayerCurrentMp; }
    public double Get_PlayerCurrentMp()
    { return PlayerCurrentMp; }
    public void Set_PlayerMaxMp(double Set_PlayerMaxMp)
    { PlayerMaxMp = Set_PlayerMaxMp; }
    public double Get_PlayerMaxMp()
    { return PlayerMaxMp; }
    public static double Calc_Dmg(double currentHpfromtaker, double dmgfrommaker)
    {
        return 1;
        //return currentHpfromtaker -= dmgfrommaker;
    }
    public static float get_RandomFloat(int smallest, int biggest)
    {
        Random rng = new Random();
        double RandomNumber = rng.Next(smallest, biggest);
        return (float)RandomNumber;
    }

}
