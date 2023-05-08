using Godot;
using System;

public class Dummy : Node2D
{

    AnimatedSprite hitEffect;
    Label hitText;
    int hitCount;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        hitEffect = GetNode<AnimatedSprite>("AnimatedSprite");
        hitText = GetNode<Label>("Label");
        hitCount = 0;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }

    public void OnDummyAreaEntered(Area2D area)
    {
        hitEffect.Frame = 0;
        hitEffect.Visible = true;
        hitEffect.Play();
        hitCount++;
        hitText.Text = ("Hits " + hitCount);
    }

    public void OnAnimationFinished()
    {
        hitEffect.Visible = false;
    }
}
