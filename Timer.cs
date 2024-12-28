using Godot;
using System;

public partial class Timer : Godot.Timer
{
    public void OnTimer()
    {
        AnimationPlayer player = GetNode<AnimationPlayer>("../Mage/AnimationPlayer");
        AnimationPlayer player2 = GetNode<AnimationPlayer>("../Knight/AnimationPlayer");
        AnimationPlayer player3 = GetNode<AnimationPlayer>("../Rogue_Hooded/AnimationPlayer");

        player.Play("Spellcast_Shoot");
        player2.Play("1H_Melee_Attack_Slice_Diagonal");
        player3.Play("2H_Ranged_Shoot");
    }
}
