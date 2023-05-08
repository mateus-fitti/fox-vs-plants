using Godot;
using System;

public class Projectile : Area2D
{
    [Export] public int ProjectileSpeed = 250;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        Vector2 direction = Vector2.Right.Rotated(Rotation);
        GlobalPosition += direction * ProjectileSpeed * delta;
    }

    public void OnAreaEntered(Area2D area)
    {
        QueueFree();
    }

    public void OnAreaEntered(Node body)
    {
        QueueFree();
    }

}
