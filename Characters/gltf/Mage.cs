using Godot;
using System;

public partial class Mage : Node3D
{
    public void Ready()
    {
        AnimationPlayer player = GetNode<AnimationPlayer>("AnimationPlayer");

        player.CurrentAnimation = "Spellcast_Raise";
        player.Play("Spellcast_Shoot");
    }
}
